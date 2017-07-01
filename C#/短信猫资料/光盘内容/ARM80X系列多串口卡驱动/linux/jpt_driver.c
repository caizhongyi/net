#include <linux/config.h>
#include <linux/module.h>
#include <linux/version.h>
#include <linux/errno.h>
#include <linux/signal.h>
#include <linux/sched.h>
#include <linux/timer.h>
#include <linux/interrupt.h>
#include <linux/tty.h>
#include <linux/tty_flip.h>
#include <linux/serial.h>
#include <linux/serial_reg.h>
#include <linux/major.h>
#include <linux/string.h>
#include <linux/fcntl.h>
#include <linux/fs.h>
#include <linux/ptrace.h>
#include <linux/ioport.h>
#include <linux/mm.h>
#include <linux/smp_lock.h>
#include <linux/pci.h>
#include <linux/timer.h>
#include <linux/syscalls.h>

#include <asm/system.h>
#include <asm/io.h>
#include <asm/irq.h>
#include <asm/bitops.h>
#include <asm/uaccess.h>
#include <asm/delay.h>

#include "jpt_core.h"

#define		NUART_VERSION		"1.0"

#define	PCI_VENDOR_ID_NUART		0x10EE
#define PCI_DEVICE_ID_NUART		0x0100

#define		NUARTMAJOR 		184

#define 	NUART_BOARDS		5	/* Max. boards */
#define 	NUART_PORTS		80	/* Max. ports */
#define 	NUART_PORTS_PER_BOARD	16	/* Max. ports per board */

#define WAKEUP_CHARS		256	

#define	VERSION_CODE(ver,rel,seq) ((ver << 16) | (rel << 8) | seq)

static char *nuart_typename[] = {
	"rs232",
	"rs485",
	"rs422/rs485",
	"rs232/rs422/rs485"
};

struct nuart_hwconf {
	int board_type;
	int ports;
	unsigned long ioaddr_base;	
	long baud_base;
	struct pci_dev *pdev;
};
struct nuart_struct 
{
	int port;
	unsigned long base;	/* port base address */
	int baud_base;		/* max. speed */
	int type;
	int mode;
	int flags;		/* defined in tty.h */

	struct tty_struct *tty;
	int x_char;		/* xon/xoff character */
	int close_delay;
	unsigned short closing_wait;
	unsigned long	event;
	int count;		/* # of fd on device */
	int blocked_open;	/* # of blocked opens */
	int xmit_fifo_size;

	unsigned char *xmit_buf;
	int xmit_head;
	int xmit_tail;
	int xmit_cnt;

	unsigned char	rrp;
	unsigned char	rwp;
	unsigned char	trp;
	unsigned char	twp;
	
	char 	wbl[4];
	
	/* register shadow */
	struct nuart_reg  reg;
	unsigned long read_status_mask;
	unsigned long ignore_status_mask;

#if (LINUX_VERSION_CODE < VERSION_CODE(2,6,0))	
	struct tq_struct	tqueue;
#else
	struct work_struct tqueue;
#endif	
	int cflag;
	unsigned long lflag;	/* local flag */

#if (LINUX_VERSION_CODE < VERSION_CODE(2,4,0))
	struct wait_queue	*open_wait;
	struct wait_queue	*close_wait;
	struct wait_queue	*delta_msr_wait;
#else
	wait_queue_head_t open_wait;
	wait_queue_head_t close_wait;
	wait_queue_head_t delta_msr_wait;
#endif

	struct async_icount icount;	
	spinlock_t		lock;
	long			session;	/* Session of opening process */
	long			pgrp;		/* pgrp of opening process */
	int			timeout;

	int fifo_size;
	
	void (* receive_func)(struct nuart_struct *, unsigned long);
	void (* transmit_func)(struct nuart_struct *);
};

/* local flag */
#define		NUART_LFLAG_MSI		0x01
#define		NUART_LFLAG_RLSI	0x02
#define 	NUART_LFLAG_THRI	0x04
#define 	NUART_LFLAG_RDI		0x08

/* REGISTER */
#define		NUART_PCR		0x200
#define		NUART_XPR		0x204
#define		NUART_BR		0x208
#define		NUART_MPR		0x20C
#define		NUART_PR		0x210

/* NUART_PCR */
#define		NUART_CHE		(1<<31)
#define		NUART_OCLR		(1<<26)
#define		NUART_OSUSP		(1<<25)
#define		NUART_PXOFF		(1<<24)
#define		NUART_PXON		(1<<23)
#define		NUART_BC		(1<<22)
#define		NUART_BZO		(1<<21)
#define		NUART_BZ		(1<<20)
#define		NUART_ERRC		(1<<19)
#define		NUART_RXX		(1<<18)
#define		NUART_IXA		(1<<17)
#define		NUART_IX		(1<<16)
#define		NUART_CXOFF		(1<<15)
#define		NUART_OX		(1<<14)
#define		NUART_OCTS		(1<<13)
#define		NUART_ODSR		(1<<12)

#define		NUART_RTSC_LOW	0
#define		NUART_RTSC_HIGH	(1<<10)
#define		NUART_RTSC_FLOW	(2<<10)
#define		NUART_RTSC_TOG	(3<<10)
#define		NUART_RTSC_MASK	(3<<10)

#define		NUART_DTRC_LOW	0
#define		NUART_DTRC_HIGH	(1<<8)
#define		NUART_DTRC_FLOW	(2<<8)
#define		NUART_DTRC_TOG	(3<<8)
#define		NUART_DTRC_MASK	(3<<8)

#define		NUART_OBRK		(1<<7)

#define		NUART_P_ODD		0
#define		NUART_P_EVEN	(1<<5)
#define		NUART_P_MARK	(2<<5)
#define		NUART_P_SPACE	(3<<5)
#define		NUART_P_MASK	(3<<5)

#define		NUART_PEN		(1<<4)
#define		NUART_SBITS		(1<<3)

/* NUART_MPR */
#define		NUART_RXOFF		(1<<24)
#define		NUART_TBUSY		(1<<23)
#define		NUART_PERR		(1<<22)
#define		NUART_FERR		(1<<21)
#define		NUART_IBRK		(1<<20)
#define		NUART_DCD		(1<<19)
#define		NUART_RI		(1<<18)
#define		NUART_DSR		(1<<17)
#define		NUART_CTS		(1<<16)


#define RELEVANT_IFLAG(iflag)	(iflag & (IGNBRK|BRKINT|IGNPAR|PARMRK|INPCK))
#define 	PORTNO(x)	((x)->index)
#ifndef MIN
#define MIN(a,b)	((a) < (b) ? (a) : (b))
#endif

#define SERIAL_TYPE_NORMAL	1

#define NUART_EVENT_TXLOW	1
#define NUART_EVENT_HANGUP	2

static int ttymajor = NUARTMAJOR;
static int verbose = 1;

/* Variables for insmod */

MODULE_AUTHOR("newtry");
MODULE_DESCRIPTION("NewTry");
MODULE_LICENSE("GPL");
MODULE_PARM(ttymajor, "i");
MODULE_PARM(verbose, "i");


static struct tty_driver *nuvar_sdriver;
static struct nuart_struct nuvar_table[NUART_PORTS];
static unsigned char *nuvar_tmp_buf;
static struct semaphore nuvar_tmp_buf_sem;
static struct timer_list nuart_timer;

struct nuart_hwconf nuartcfg[NUART_BOARDS];

/* baud max 1.8432MHZ */
static int nuart_baud_table[] =
{
	0, 50, 75, 110, 134, 150, 200, 300, 600, 1200, 1800, 2400, 4800,
	9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600, 0 
};

static struct pci_device_id nuart_pcibrds[] = {
	{ PCI_VENDOR_ID_NUART, PCI_DEVICE_ID_NUART, PCI_ANY_ID, PCI_ANY_ID, 0, 0, NUART_BOARD_RS232},
	{ 0 }
};
MODULE_DEVICE_TABLE(pci, nuart_pcibrds);


static int nuart_open(struct tty_struct *, struct file *);
static void nuart_close(struct tty_struct *, struct file *);
#if (LINUX_VERSION_CODE < VERSION_CODE(2,6,10))
static int nuart_write(struct tty_struct *, int, const unsigned char *, int);
#else
static int nuart_write(struct tty_struct *, const unsigned char *, int);
#endif
static int nuart_write_room(struct tty_struct *);
static void nuart_flush_buffer(struct tty_struct *);
static int nuart_chars_in_buffer(struct tty_struct *);
static void nuart_flush_chars(struct tty_struct *);
static void nuart_put_char(struct tty_struct *, unsigned char);
static int nuart_ioctl(struct tty_struct *, struct file *, uint, ulong);
static void nuart_throttle(struct tty_struct *);
static void nuart_unthrottle(struct tty_struct *);
static void nuart_set_termios(struct tty_struct *, struct termios *);
static void nuart_stop(struct tty_struct *);
static void nuart_start(struct tty_struct *);
static void nuart_hangup(struct tty_struct *);
static void nuart_receive_chars(struct nuart_struct *, unsigned long);
static void nuart_transmit_chars(struct nuart_struct *);
static void nuart16_receive_chars(struct nuart_struct *, unsigned long);
static void nuart16_transmit_chars(struct nuart_struct *);
static int nuart_block_til_ready(struct tty_struct *, struct file *, struct nuart_struct *);
static int nuart_startup(struct nuart_struct *);
static void nuart_shutdown(struct nuart_struct *);
static int nuart_change_speed(struct nuart_struct *, struct termios *old_termios);
static int nuart_get_serial_info(struct nuart_struct *, struct serial_struct *);
static int nuart_set_serial_info(struct nuart_struct *, struct serial_struct *);
static int nuart_get_lsr_info(struct nuart_struct *, unsigned int *);
static void nuart_send_break(struct nuart_struct *, int);
static int nuart_tiocmget(struct tty_struct * tty, struct file * file);
static int nuart_tiocmset(struct tty_struct *tty, struct file * file, unsigned int set, unsigned int clear);
//static int nuart_get_modem_info(struct nuart_struct *, unsigned int *);
//static int nuart_set_modem_info(struct nuart_struct *, unsigned int, unsigned int *);
static void nuart_timeout(unsigned long ptr);
static void nuart_do_softint(void *private_);
static int nuart_pci_probe(void);

static void nuart_check_modem_status(struct nuart_struct *info, unsigned long status)
{
	int delta_dcd = 0;

	if((info->reg.mpr & NUART_RI) != (status & NUART_RI))
	    info->icount.rng++;
	if((info->reg.mpr & NUART_DSR) != (status & NUART_DSR))
	    info->icount.dsr++;
	if((info->reg.mpr & NUART_DCD) != (status & NUART_DCD))
	{
	    info->icount.dcd++;
		delta_dcd = 1;
	}
	if((info->reg.mpr & NUART_CTS) != (status & NUART_CTS))
	    info->icount.cts++;

	wake_up_interruptible(&info->delta_msr_wait);

	if ( (info->flags & ASYNC_CHECK_CD) && delta_dcd ) 
	{
	    if ( status & NUART_DCD )
		wake_up_interruptible(&info->open_wait);
	    else
	    {
		set_bit(NUART_EVENT_HANGUP, &info->event);
	    	schedule_work(&info->tqueue);
	    }
	}

	if ( info->flags & ASYNC_CTS_FLOW ) 
	{
	    if ( info->tty->hw_stopped ) 
	    {
		if (status & NUART_CTS )
		{  
			info->tty->hw_stopped = 0;
			set_bit(NUART_EVENT_TXLOW, &info->event);
	   		schedule_work(&info->tqueue);
        	}   
	    }
	    else
	    {
		if ( !(status & NUART_CTS) )
		{
		    info->tty->hw_stopped = 1;
	   	}
	    }
	}
}


static void nuart_receive_chars(struct nuart_struct *info, unsigned long stat)
{
	struct tty_struct *tty = info->tty;
	unsigned char rbuf[260];
	unsigned long tmprrp;
	int count, pad, i;
	unsigned long	status = stat;
	unsigned long	flags = 0;

	count = (info->rwp - info->rrp) & 0xff;
	count = MIN(count, TTY_FLIPBUF_SIZE - tty->flip.count);
	if(!count)
		return;

	pad = info->rrp & 3;
	tmprrp = info->rrp & (~3);

	spin_lock_irqsave(&info->lock, flags);
	nuart_read_buf(info->base,tmprrp, rbuf, pad+count);
	spin_unlock_irqrestore(&info->lock, flags);
	
	for (i = pad; i < count + pad; i++)
	{
		tty->flip.count++;

		if(status & (NUART_PERR | NUART_FERR | NUART_IBRK))
		{
			if (status & NUART_IBRK) 
			{
				*tty->flip.flag_buf_ptr++ = TTY_BREAK;
				info->icount.brk++;

				if ( info->flags & ASYNC_SAK )
				    do_SAK(tty);

				status &= ~NUART_IBRK;
			} 
			else if (status & NUART_PERR) 
			{
				*tty->flip.flag_buf_ptr++ = TTY_PARITY;
				info->icount.parity++;
				status &= ~NUART_PERR;
			} 
			else if (status & NUART_FERR) 
			{
				*tty->flip.flag_buf_ptr++ = TTY_FRAME;
				info->icount.frame++;
				status &= ~NUART_FERR;
			}
			else
				*tty->flip.flag_buf_ptr++ = TTY_NORMAL;
		} else
				*tty->flip.flag_buf_ptr++ = TTY_NORMAL;
	
		*tty->flip.char_buf_ptr++ = rbuf[i];
	}

	info->icount.rx += count;
	info->rrp += count;
//	schedule_delayed_work(&tty->flip.work, 1);
	tty_flip_buffer_push(tty);

	spin_lock_irqsave(&info->lock, flags);
	nuart_set_rrp(info->base, &info->reg, info->rrp);

	if(stat & (NUART_PERR | NUART_FERR | NUART_IBRK))
		nuart_clr_err(info->base, &info->reg);

	spin_unlock_irqrestore(&info->lock, flags);

	return;
}
static void nuart16_receive_chars(struct nuart_struct *info, unsigned long stat)
{
	struct tty_struct *tty = info->tty;
	unsigned char rbuf[260];
	unsigned long tmprrp;
	int count, pad, i;
	unsigned long	status = stat;
	unsigned long	flags = 0;

	count = (info->rwp - info->rrp) & 0x7f;
	count = MIN(count, TTY_FLIPBUF_SIZE - tty->flip.count);
	if(!count)
		return;

	pad = info->rrp & 3;
	tmprrp = info->rrp & (~3);

	
	spin_lock_irqsave(&info->lock, flags);
	nuart_read_buf(info->base,tmprrp, rbuf, pad+count);
	spin_unlock_irqrestore(&info->lock, flags);
	
	for (i = pad; i < count + pad; i++)
	{
		tty->flip.count++;

		if(status & (NUART_PERR | NUART_FERR | NUART_IBRK))
		{
			if (status & NUART_IBRK) 
			{
				*tty->flip.flag_buf_ptr++ = TTY_BREAK;
				info->icount.brk++;

				if ( info->flags & ASYNC_SAK )
				    do_SAK(tty);

				status &= ~NUART_IBRK;
			} 
			else if (status & NUART_PERR) 
			{
				*tty->flip.flag_buf_ptr++ = TTY_PARITY;
				info->icount.parity++;
				status &= ~NUART_PERR;
			} 
			else if (status & NUART_FERR) 
			{
				*tty->flip.flag_buf_ptr++ = TTY_FRAME;
				info->icount.frame++;
				status &= ~NUART_FERR;
			}
			else
				*tty->flip.flag_buf_ptr++ = TTY_NORMAL;
		} else
				*tty->flip.flag_buf_ptr++ = TTY_NORMAL;
	
		*tty->flip.char_buf_ptr++ = rbuf[i];
	}

	info->icount.rx += count;
	info->rrp += count;
//	schedule_delayed_work(&tty->flip.work, 1);
	tty_flip_buffer_push(tty);

	spin_lock_irqsave(&info->lock, flags);
	nuart_set_rrp(info->base, &info->reg, info->rrp);

	if(stat & (NUART_PERR | NUART_FERR | NUART_IBRK))
		nuart_clr_err(info->base, &info->reg);

	spin_unlock_irqrestore(&info->lock, flags);

	return;
}

static void nuart_transmit_chars(struct nuart_struct *info)
{
	int count, i;
	int wbl_pos;
	int buf_pos;
	char  tbuf[256];
	int c;
	unsigned long flags = 0;

	count = 248 - ((info->twp - info->trp) & 0xff);
	count = MIN(info->xmit_cnt, count);

	if(count <= 0)
		return;

	wbl_pos = info->twp & 3;
	buf_pos = info->twp & (~3);
	if(wbl_pos)
		memcpy(tbuf,info->wbl,wbl_pos);
	
	for(i = wbl_pos;i < count + wbl_pos; i++)
	{
		c = info->xmit_buf[info->xmit_tail++];
		info->xmit_tail = info->xmit_tail & (SERIAL_XMIT_SIZE - 1);
		info->wbl[i & 3] = c;
		tbuf[i] = c;
	}
	

	spin_lock_irqsave(&info->lock, flags);
	nuart_write_buf(info->base, buf_pos, tbuf, wbl_pos + count);
	spin_unlock_irqrestore(&info->lock, flags);

	info->xmit_cnt -= count;

	info->twp += count;
	
	spin_lock_irqsave(&info->lock, flags);
	nuart_set_twp(info->base, &info->reg, info->twp);
	spin_unlock_irqrestore(&info->lock, flags);

	if(info->xmit_cnt < WAKEUP_CHARS)
	{
		set_bit(NUART_EVENT_TXLOW, &info->event);	
		schedule_work(&info->tqueue);
	}

	return;
}
static void nuart16_transmit_chars(struct nuart_struct *info)
{
	int count, i;
	int wbl_pos;
	int buf_pos;
	char  tbuf[256];
	int c;
	unsigned long flags = 0;

	count = 120 - ((info->twp - info->trp) & 0x7f);
	count = MIN(info->xmit_cnt, count);

	if(count <= 0)
		return;

	wbl_pos = info->twp & 3;
	buf_pos = info->twp & (~3);
	if(wbl_pos)
		memcpy(tbuf,info->wbl,wbl_pos);
	
	for(i = wbl_pos;i < count + wbl_pos; i++)
	{
		c = info->xmit_buf[info->xmit_tail++];
		info->xmit_tail = info->xmit_tail & (SERIAL_XMIT_SIZE - 1);
		info->wbl[i & 3] = c;
		tbuf[i] = c;
	}
	

	spin_lock_irqsave(&info->lock, flags);
	nuart_write_buf(info->base, buf_pos, tbuf, wbl_pos + count);
	spin_unlock_irqrestore(&info->lock, flags);

	info->xmit_cnt -= count;

	info->twp += count;
	
	spin_lock_irqsave(&info->lock, flags);
	nuart_set_twp(info->base, &info->reg, info->twp);
	spin_unlock_irqrestore(&info->lock, flags);

	if(info->xmit_cnt < WAKEUP_CHARS)
	{
		set_bit(NUART_EVENT_TXLOW, &info->event);	
		schedule_work(&info->tqueue);
	}

	return;
}

static void nuart_timeout_port(struct nuart_struct * info)
{
	unsigned long	status, flags;

	spin_lock_irqsave(&info->lock, flags);
	status = nuart_get_stat(info->base);
	spin_unlock_irqrestore(&info->lock, flags);

	info->rwp = ((status & 0xff00) >> 8) & 0xff;
	info->trp = status & 0xff;

	if((status & (NUART_RI | NUART_DSR | NUART_DCD | NUART_CTS)) != 
		(info->reg.mpr & (NUART_RI | NUART_DSR | NUART_DCD | NUART_CTS)))
		nuart_check_modem_status(info, status);

	nuart_receive_chars(info, status);
	info->reg.mpr = status;

	if (info->xmit_cnt && !info->tty->stopped && !info->tty->hw_stopped)
		nuart_transmit_chars(info);
		
}
static void nuart_timeout_single(int board, struct nuart_hwconf *hwconf)
{
	struct nuart_struct *info;
	int i, n;

	n = board * NUART_PORTS_PER_BOARD;
	info = &nuvar_table[n];

	for (i = 0; i < hwconf->ports; i++, n++, info++) 
	{
		if(info->port != n)
			continue;
		if (info->flags & ASYNC_INITIALIZED)
			nuart_timeout_port(info);
	}
}

static void nuart_timeout(unsigned long ptr)
{
	int i;

	for (i = 0; i < NUART_BOARDS; i++) 
	{
		if (nuartcfg[i].board_type == -1)
			continue;

		nuart_timeout_single(i, &nuartcfg[i]);
	}

	nuart_timer.function = nuart_timeout;
	nuart_timer.data = (unsigned long)0;
	nuart_timer.expires = jiffies + 1;  /* 1ms */

	add_timer(&nuart_timer);
	return;
}

static int nuart_open(struct tty_struct *tty, struct file *filp)
{
	struct nuart_struct *info;
	int retval, line;
	unsigned long page;

	line = PORTNO(tty);
	if ((line < 0) || (line >= NUART_PORTS))
		return (-ENODEV);

	info = &nuvar_table[line];
	if (!info->base)
		return (-ENODEV);

	tty->driver_data = info;
	info->tty = tty;

	if (!nuvar_tmp_buf) {
		page = get_zeroed_page(GFP_KERNEL);
		if (!page)
			return (-ENOMEM);
		if (nuvar_tmp_buf)
			free_page(page);
		else
			nuvar_tmp_buf = (unsigned char *) page;
	}
	/*
	 * Start up serial port
	 */
	info->count++;
	retval = nuart_startup(info);
	if (retval)
		return (retval);

	return nuart_block_til_ready(tty, filp, info);
}

static int nuart_get_line(struct file * f, char * buf, ssize_t size, loff_t * off)
{
	int n, i;
	loff_t offset = *off;

	n = f->f_op->read(f, buf, size, &offset);

	if(n <= 0)
		return 0;

	for(i = 0; i < n ; i++)
	{
		if(buf[i] == 0xa)
			break;
	}

	buf[i] = 0;
	i++;
	*off += i;	

	return i;
}

static int nuart_atoi(char * s)
{
	int n = 0;
	char * p = s;

	while((*p == ' ') || (*p == '\t'))
		p ++;

	while((*p >= '0') && (*p <= '9'))
	{
		n *= 10;
		n += *p - '0';
		p ++;
	}
	return n;
} 

static void nuart_get_mode(struct nuart_struct * info)
{
	struct file * f;	
	char	buf[256];
	char	*s;
	mm_segment_t fs;
	loff_t offset = 0;

	if(info->type == NUART_BOARD_RS232)
	{
		info->mode = NUART_PORT_RS232;
		return;
	}

	f = filp_open("/etc/jpt.conf",O_RDWR, 0600);
	if(IS_ERR(f))
	{
		printk("WARNING : /etc/jpt.conf does not exist\n");
		info->mode = -1;
	}
	else
	{
		info->mode = -1;
		fs = get_fs();
		set_fs(KERNEL_DS);
		while(nuart_get_line(f, buf, sizeof(buf), &offset))
		{
			if(memcmp(buf, "ttyn", 4))
				continue;

			s = buf + 4;

			if(nuart_atoi(s) != info->port)
				continue;

			while((*s != ' ') && (*s != '\t'))
				s++;
		
			info->mode = nuart_atoi(s);
			break;
		}
		set_fs(fs);

		filp_close(f, NULL);
	}


	switch(info->type)
	{
	case NUART_BOARD_RS232:
		break;
	case NUART_BOARD_RS485:
		if((info->mode != NUART_PORT_RS485F) && (info->mode != NUART_PORT_RS485H))
		{
			printk("WARNING : set ttyn%d to default mode(rs_485 full)\n", info->port);
			info->mode = NUART_PORT_RS485F;
		}
		break;
	case NUART_BOARD_RS422_RS485:
		if((info->mode != NUART_PORT_RS422) && (info->mode != NUART_PORT_RS485F) && (info->mode != NUART_PORT_RS485H))
		{
			printk("WARNING : set ttyn%d to default mode(rs_485 full)\n", info->port);
			info->mode = NUART_PORT_RS485F;
		}
		break;
	case NUART_BOARD_RS232_RS422_RS485:
		if((info->mode != NUART_PORT_RS232) && (info->mode != NUART_PORT_RS422) && (info->mode != NUART_PORT_RS485F) && (info->mode != NUART_PORT_RS485H))
		{
			printk("WARNING : set ttyn%d to default mode(rs_232)\n", info->port);
			info->mode = NUART_PORT_RS232;
		}
		break;
	default:
		printk("WARNING : set ttyn%d to default mode(rs_232)\n", info->port);
		info->mode = NUART_PORT_RS232;
	}

	return;
}

static int nuart_startup(struct nuart_struct *info)
{
	unsigned long flags = 0;
	unsigned long page;

	page = get_zeroed_page(GFP_KERNEL);
	if (!page)
		return (-ENOMEM);

	spin_lock_irqsave(&info->lock, flags);
	if (info->flags & ASYNC_INITIALIZED) {
		free_page(page);
		spin_unlock_irqrestore(&info->lock, flags);
		return (0);
	}

	if (!info->base) 
	{
		if (info->tty)
			set_bit(TTY_IO_ERROR, &info->tty->flags);
		free_page(page);
		spin_unlock_irqrestore(&info->lock, flags);
		return (0);
	}
	if (info->xmit_buf)
		free_page(page);
	else
		info->xmit_buf = (unsigned char *) page;
	
	/* initialize the port */ 
	nuart_get_mode(info);	
	nuart_port_enable_and_init(info->base, &info->reg, info->mode);
	nuart_dtr(info->base, &info->reg, 1);
	nuart_rts(info->base, &info->reg, 1);

	info->lflag = (NUART_LFLAG_RDI | NUART_LFLAG_MSI | NUART_LFLAG_THRI | NUART_LFLAG_RLSI);

	if (info->tty)
		test_and_clear_bit(TTY_IO_ERROR, &info->tty->flags);

	info->xmit_cnt = info->xmit_head = info->xmit_tail = 0;

	spin_unlock_irqrestore(&info->lock, flags);
	/*
	 * and set the speed of the serial port
	 */
	nuart_change_speed(info, 0);

	info->flags |= ASYNC_INITIALIZED;
	return (0);
}

static int nuart_change_speed(struct nuart_struct *info, struct termios *old_termios)
{
	unsigned cflag, cval;
	int i;
	int ret = 0;
	unsigned long flags;

	unsigned long quot = 0;
	unsigned long pcr = 0;
	unsigned long pcr_mask = 0;

	if (!info->tty || !info->tty->termios)
		return ret;
	cflag = info->tty->termios->c_cflag;
	if (!(info->base))
		return ret;


#ifndef B921600
#define B921600 (B460800 +1)
#endif
	switch (cflag & (CBAUD | CBAUDEX)) 
	{
	case B921600:
		i = 20;
		break;
	case B460800:
		i = 19;
		break;
	case B230400:
		i = 18;
		break;
	case B115200:
		i = 17;
		break;
	case B57600:
		i = 16;
		break;
	case B38400:
		i = 15;
		break;
	case B19200:
		i = 14;
		break;
	case B9600:
		i = 13;
		break;
	case B4800:
		i = 12;
		break;
	case B2400:
		i = 11;
		break;
	case B1800:
		i = 10;
		break;
	case B1200:
		i = 9;
		break;
	case B600:
		i = 8;
		break;
	case B300:
		i = 7;
		break;
	case B200:
		i = 6;
		break;
	case B150:
		i = 5;
		break;
	case B134:
		i = 4;
		break;
	case B110:
		i = 3;
		break;
	case B75:
		i = 2;
		break;
	case B50:
		i = 1;
		break;
	default:
		i = 0;
		break;
	}

	if(i == 15)
	{
		if((info->flags & ASYNC_SPD_MASK) == ASYNC_SPD_HI)
			i = 16;
		if((info->flags & ASYNC_SPD_MASK) == ASYNC_SPD_VHI)
			i = 17;
#ifdef ASYNC_SPD_SHI
		if((info->flags & ASYNC_SPD_MASK) == ASYNC_SPD_SHI)
			i = 18;
#endif
#ifdef ASYNC_SPD_WARP
		if((info->flags & ASYNC_SPD_MASK) == ASYNC_SPD_WARP)
			i = 19;
#endif
	}

	if (nuart_baud_table[i]) 
		quot = info->baud_base / nuart_baud_table[i];
	else 
		quot = 0;

	if(quot)
		quot --;

	/* byte size and parity */
	pcr = 0;
	cval = 9;
	if (cflag & CSTOPB)
	{
		pcr |= NUART_SBITS;
	}

	if (cflag & PARENB)
	{
		pcr |= NUART_PEN;
		if (!(cflag & PARODD))
			pcr |= NUART_P_EVEN;
		cval -= 1;
	}

	switch (cflag & CSIZE) 
	{
	case CS5:
		pcr |= cval  - 5;;
		break;
	case CS6:
		pcr |= cval  - 6;;
		break;
	case CS7:
		pcr |= cval  - 7;
		break;
	case CS8:
		pcr |= cval  - 8;
		break;
	default:
		break;		/* too keep GCC shut... */
	}

	if (cflag & CRTSCTS) 
	{
		if(!(info->reg.pcr & NUART_RTSC_FLOW))
		{
			info->flags |= ASYNC_CTS_FLOW;
			pcr_mask |= NUART_RTSC_MASK;
			pcr |= (NUART_OCTS | NUART_RTSC_FLOW);
		}
	} 

	
	if(info->tty->termios->c_iflag & IXON)
	{
		nuart_set_xchar(info->base, &info->reg, START_CHAR(info->tty), STOP_CHAR(info->tty));
		pcr |= (NUART_OX | NUART_IX);
	}
	else
	{
		pcr_mask |= (NUART_OX | NUART_IX);
	}

	if (cflag & CLOCAL)
		info->flags &= ~ASYNC_CHECK_CD;
	else 
		info->flags |= ASYNC_CHECK_CD;

	/* set up parity check flag */

	info->read_status_mask = 0;
	if(I_INPCK(info->tty))
		info->read_status_mask |= (NUART_FERR | NUART_PERR);
	if(I_BRKINT(info->tty) || I_PARMRK(info->tty))
		info->read_status_mask |= NUART_IBRK;

	info->ignore_status_mask = 0;
	if(I_IGNBRK(info->tty))
	{
		info->read_status_mask |= NUART_IBRK;
		info->ignore_status_mask |= NUART_IBRK;
		if(I_IGNPAR(info->tty))
		{
			info->ignore_status_mask |= (NUART_PERR | NUART_FERR);
			info->read_status_mask |= (NUART_PERR | NUART_FERR);
		}
	}

	if(quot == 0 || quot == 1)
	{
		pcr |= NUART_BZO;
		if(quot == 0)
			pcr |= NUART_BZ;
	}

	pcr_mask |= (NUART_P_MASK | NUART_SBITS | NUART_PEN | 7);
	info->reg.pcr &= ~pcr_mask;
	info->reg.pcr |= pcr;

	spin_lock_irqsave(&info->lock, flags);
	nuart_set_pcr(info->base, &info->reg, info->reg.pcr);
	nuart_baud(info->base, &info->reg, quot);
	spin_unlock_irqrestore(&info->lock, flags);

	return ret;
}


static int nuart_block_til_ready(struct tty_struct *tty, struct file *filp,
				 struct nuart_struct *info)
{
	DECLARE_WAITQUEUE(wait, current);
	unsigned long flags;
	int retval;
	int do_clocal = 0;

	/*
	 * If the device is in the middle of being closed, then block
	 * until it's done, and then try again.
	 */
/*
	if (tty_hung_up_p(filp) || (info->flags & ASYNC_CLOSING)) 
	{
		if (info->flags & ASYNC_CLOSING)
			interruptible_sleep_on(&info->close_wait);
#ifdef SERIAL_DO_RESTART
		if (info->flags & ASYNC_HUP_NOTIFY)
			return (-EAGAIN);
		else
			return (-ERESTARTSYS);
#else
		return (-EAGAIN);
#endif
	}
*/
	/*
	 * If non-blocking mode is set, or the port is not enabled,
	 * then make the check up front and then exit.
	 */
	if ((filp->f_flags & O_NONBLOCK) ||
	    (tty->flags & (1 << TTY_IO_ERROR))) {
		info->flags |= ASYNC_NORMAL_ACTIVE;
		return (0);
	}
	if (tty->termios->c_cflag & CLOCAL)
		do_clocal = 1;

	retval = 0;
	add_wait_queue(&info->open_wait, &wait);
	spin_lock_irqsave(&info->lock, flags);
	if (!tty_hung_up_p(filp))
		info->count--;
	spin_unlock_irqrestore(&info->lock, flags);
	info->blocked_open++;
	while (1) 
	{
		spin_lock_irqsave(&info->lock, flags);
		nuart_dtr(info->base, &info->reg, 1);
		nuart_rts(info->base, &info->reg, 1);
		spin_unlock_irqrestore(&info->lock, flags);

		set_current_state(TASK_INTERRUPTIBLE);
		if (tty_hung_up_p(filp) || !(info->flags & ASYNC_INITIALIZED)) {
#ifdef SERIAL_DO_RESTART
			if (info->flags & ASYNC_HUP_NOTIFY)
				retval = -EAGAIN;
			else
				retval = -ERESTARTSYS;
#else
			retval = -EAGAIN;
#endif
			break;
		}
		info->reg.mpr = nuart_get_stat(info->base);
		if (!(info->flags & ASYNC_CLOSING) &&
		    (do_clocal || (info->reg.mpr & NUART_DCD)))
			break;
		if (signal_pending(current)) {
			retval = -ERESTARTSYS;
			break;
		}
		schedule();
	}
	set_current_state(TASK_RUNNING);
	remove_wait_queue(&info->open_wait, &wait);
	if (!tty_hung_up_p(filp))
		info->count++;
	info->blocked_open--;
	if (retval)
		return (retval);
	info->flags |= ASYNC_NORMAL_ACTIVE;
	return (0);
}

static void nuart_close(struct tty_struct *tty, struct file *filp)
{
	struct nuart_struct *info = (struct nuart_struct *) tty->driver_data;
	unsigned long flags, timeout;

	if (PORTNO(tty) == NUART_PORTS)
		return;
	if (!info || !info->base)
		return;

	spin_lock_irqsave(&info->lock, flags);

	if (tty_hung_up_p(filp)) {
		spin_unlock_irqrestore(&info->lock, flags);
		return;
	}
	if ((tty->count == 1) && (info->count != 1)) {
		/*
		 * Uh, oh.        tty->count is 1, which means that the tty
		 * structure will be freed.  Info->count should always
		 * be one in these conditions.  If it's greater than
		 * one, we've got real problems, since it means the
		 * serial port won't be shutdown.
		 */
		printk("nuart_close: bad serial port count; tty->count is 1, "
		       "info->count is %d\n", info->count);
		info->count = 1;
	}
	if (--info->count < 0) {
		printk("nuart_close: bad serial port count for ttys%d: %d\n",
		       info->port, info->count);
		info->count = 0;
	}
	if (info->count) {
		spin_unlock_irqrestore(&info->lock, flags);
		return;
	}
	info->flags |= ASYNC_CLOSING;
	spin_unlock_irqrestore(&info->lock, flags);

	info->cflag = tty->termios->c_cflag;
	/*
	 * Now we wait for the transmit buffer to clear; and we notify
	 * the line discipline to only process XON/XOFF characters.
	 */
	tty->closing = 1;
	if (info->closing_wait != ASYNC_CLOSING_WAIT_NONE)
		tty_wait_until_sent(tty, info->closing_wait);

	if(info->flags & ASYNC_INITIALIZED)
	{
		timeout = jiffies + HZ;
		spin_lock_irqsave(&info->lock, flags);
		info->reg.mpr = nuart_get_stat(info->base);
		spin_unlock_irqrestore(&info->lock, flags);
		while(info->reg.mpr & NUART_TBUSY)
		{
			spin_lock_irqsave(&info->lock, flags);
			info->reg.mpr = nuart_get_stat(info->base);
			spin_unlock_irqrestore(&info->lock, flags);
			set_current_state(TASK_INTERRUPTIBLE);
			schedule_timeout(5);
			if(time_after(jiffies, timeout))
				break;
		}
	}

	nuart_shutdown(info);

	if (tty->driver->flush_buffer)
		tty->driver->flush_buffer(tty);
	if (tty->ldisc.flush_buffer)
		tty->ldisc.flush_buffer(tty);
	tty->closing = 0;
	info->tty = 0;
	info->event = 0;
	if (info->blocked_open) {
		if (info->close_delay) {
			set_current_state(TASK_INTERRUPTIBLE);
			schedule_timeout(info->close_delay);
		}
		wake_up_interruptible(&info->open_wait);
	}
	info->flags &= ~(ASYNC_NORMAL_ACTIVE | ASYNC_CLOSING);
	wake_up_interruptible(&info->close_wait);
}

static void nuart_shutdown(struct nuart_struct *info)
{
	unsigned long flags = 0;

	if (!(info->flags & ASYNC_INITIALIZED))
		return;

	spin_lock_irqsave(&info->lock, flags);
	/*
	 * clear delta_msr_wait queue to avoid mem leaks: we may free the irq
	 * here so the queue might never be waken up
	 */
	wake_up_interruptible(&info->delta_msr_wait);

	if (info->xmit_buf) {
		free_page((unsigned long) info->xmit_buf);
		info->xmit_buf = 0;
	}

	if(!info->tty || (info->tty->termios->c_cflag & HUPCL))
		info->reg.pcr &= ~(NUART_RTSC_HIGH | NUART_DTRC_HIGH);

	nuart_port_disable(info->base, &info->reg);

	if (info->tty)
		set_bit(TTY_IO_ERROR, &info->tty->flags);

	info->flags &= ~ASYNC_INITIALIZED;
	spin_unlock_irqrestore(&info->lock, flags);
}

int nuart_initbrd(int board, struct nuart_hwconf *hwconf)
{
	struct nuart_struct *info;
	int i, n;

	nuart_card_disable(hwconf->ioaddr_base);

	n = board * NUART_PORTS_PER_BOARD;
	info = &nuvar_table[n];

	for (i = 0; i < hwconf->ports; i++, n++, info++) 
	{
		memset(info, 0, sizeof(struct nuart_struct));
		info->port = n;
		info->base = hwconf->ioaddr_base + i*1024;
		info->type = hwconf->board_type;

		nuart_port_initial(info->base, &info->reg);
		
		info->baud_base = hwconf->baud_base;
		info->flags = 0;

		info->close_delay = 5 * HZ / 10;
		info->closing_wait = 30 * HZ;
		INIT_WORK(&info->tqueue, nuart_do_softint, info);

		info->cflag = B9600 | CS8 | CREAD | HUPCL | CLOCAL;

		init_waitqueue_head(&info->open_wait);
		init_waitqueue_head(&info->close_wait);
		init_waitqueue_head(&info->delta_msr_wait);
		
		spin_lock_init(&info->lock);
	
		if(hwconf->ports >= 16)
		{
			info->fifo_size = 128;
			info->receive_func = nuart16_receive_chars;
			info->transmit_func = nuart16_transmit_chars;
		}
		else
		{
			info->fifo_size = 256;
			info->receive_func = nuart_receive_chars;
			info->transmit_func = nuart_transmit_chars;
		}
	}

	nuart_card_enable(hwconf->ioaddr_base);
	return 0;
}

static int nuart_pci_probe(void)
{
	struct pci_dev *pdev = NULL;
	struct nuart_hwconf hwconf;
	int n, b;
	int	card_found = 0;
	unsigned long ioaddress;	
	u32 id;

	n = (sizeof(nuart_pcibrds) / sizeof(nuart_pcibrds[0])) - 1;

	for (b = 0; b < n; b++) 
	{
		while ((pdev = pci_find_device(nuart_pcibrds[b].vendor, nuart_pcibrds[b].device, pdev)))
		{
			if (pci_enable_device(pdev))
				continue;
		
			printk("Found Jpt board(BusNo=%d,DevNo=%d)\n", pdev->bus->number, PCI_SLOT(pdev->devfn));

			if(card_found >= NUART_BOARDS)
			{
				printk("Too many Jpt Serial boards find (maximum %d),board not configured\n",NUART_BOARDS);
				goto PROBE_OUT;
			}

			memset(&hwconf, 0, sizeof(struct nuart_hwconf));

			hwconf.pdev = pdev;
			ioaddress = pci_resource_start (pdev, 0);
			if(!ioaddress)
				continue;
			hwconf.ioaddr_base = (unsigned long)ioremap(ioaddress, 65536);
			if (!hwconf.ioaddr_base)
				continue;
	
			pci_read_config_dword(pdev, 0x2c, &id);
			nuart_get_config(id, &hwconf.board_type, &hwconf.ports);	

			if(!hwconf.ports)
				continue;
			if(hwconf.board_type == -1)
				continue;

			if(hwconf.ports <= 8)
				hwconf.baud_base = 14745600/8;
			else
				hwconf.baud_base = 14745600/hwconf.ports;

			printk("type = %s, port = %d, baud base = %ld\n", nuart_typename[hwconf.board_type], hwconf.ports, hwconf.baud_base);

			if (nuart_initbrd(card_found, &hwconf) < 0)
				continue;

			memcpy(&nuartcfg[card_found], &hwconf, sizeof(struct nuart_hwconf));
			card_found ++;
		}
	}

PROBE_OUT:
	return card_found;
}


static struct tty_operations nuart_ops = {
	.open = nuart_open,
	.close = nuart_close,
	.write = nuart_write,
	.put_char = nuart_put_char,
	.flush_chars = nuart_flush_chars,
	.write_room = nuart_write_room,
	.chars_in_buffer = nuart_chars_in_buffer,
	.flush_buffer = nuart_flush_buffer,
	.ioctl = nuart_ioctl,
	.throttle = nuart_throttle,
	.unthrottle = nuart_unthrottle,
	.set_termios = nuart_set_termios,
	.stop = nuart_stop,
	.start = nuart_start,
	.hangup = nuart_hangup,
	.tiocmget = nuart_tiocmget, 
	.tiocmset = nuart_tiocmset,
};

static int __init nuart_module_init(void)
{
	int i, m;

	nuvar_sdriver = alloc_tty_driver(NUART_PORTS + 1);
	if (!nuvar_sdriver)
		return -ENOMEM;

	printk("Jpt serial driver version %s\n", NUART_VERSION);

	/* Initialize the tty_driver structure */

	nuvar_sdriver->owner = THIS_MODULE;
	nuvar_sdriver->name = "ttyN";
	nuvar_sdriver->devfs_name = "tts/M";
	nuvar_sdriver->major = ttymajor;
	nuvar_sdriver->minor_start = 0;
	nuvar_sdriver->type = TTY_DRIVER_TYPE_SERIAL;
	nuvar_sdriver->subtype = SERIAL_TYPE_NORMAL;
	nuvar_sdriver->init_termios = tty_std_termios;
	nuvar_sdriver->init_termios.c_cflag = B9600 | CS8 | CREAD | HUPCL | CLOCAL;
	nuvar_sdriver->flags = TTY_DRIVER_REAL_RAW;
	tty_set_operations(nuvar_sdriver, &nuart_ops);
	printk("Tty devices major number = %d\n", ttymajor);

	memset(nuvar_table, 0, NUART_PORTS * sizeof(struct nuart_struct));

	for (i = 0; i < NUART_BOARDS; i++) 
	{
		nuartcfg[i].board_type = -1;
	}

	m = 0;
	/* start finding PCI board here */
#ifdef CONFIG_PCI
	m = nuart_pci_probe();
#endif

	if(m == 0)
		printk("No Jpt card founded in yout system!\n");
	if(m <= 0)
		goto init_err_out;

	init_MUTEX(&nuvar_tmp_buf_sem);

	init_timer(&nuart_timer);

	if (!tty_register_driver(nuvar_sdriver))
	{
		nuart_timer.function = nuart_timeout;
		nuart_timer.data = (unsigned long)0;
		nuart_timer.expires = jiffies + 1;
		add_timer(&nuart_timer);
		return 0;
	}

	printk("Couldn't install Jpt driver !\n");

init_err_out:
	put_tty_driver(nuvar_sdriver);

	for (i = 0; i < NUART_BOARDS; i++) 
	{
		if (nuartcfg[i].board_type == -1)
			continue;
		if (nuartcfg[i].ioaddr_base)
			iounmap((void *)(nuartcfg[i].ioaddr_base));
	}
	return -1;
}

static void __exit nuart_module_exit(void)
{
	int i, err = 0;

	if (verbose)
		printk("Unloading module Jpt...");
	del_timer(&nuart_timer);

	if ((err |= tty_unregister_driver(nuvar_sdriver)))
		printk("Couldn't unregister Jpt serial driver\n");

	put_tty_driver(nuvar_sdriver);

	for (i = 0; i < NUART_BOARDS; i++) 
	{
		if (nuartcfg[i].board_type == -1)
			continue;

		if(nuartcfg[i].ioaddr_base)
		{
			iounmap((void *)(nuartcfg[i].ioaddr_base));
		}
	}

	if (verbose)
		printk("Done.\n");

}

#if (LINUX_VERSION_CODE < VERSION_CODE(2,6,10))
static int nuart_write(struct tty_struct *tty, int from_user, const unsigned char *buf, int count)
{
	struct nuart_struct *info = (struct nuart_struct *) tty->driver_data;
	int c, total = 0;
	unsigned long flags;

	if (!tty || !info->xmit_buf || !nuvar_tmp_buf)
		return (0);

	if (from_user) 
	{
		down(&nuvar_tmp_buf_sem);
		while (1) {
			c = MIN(count, MIN(SERIAL_XMIT_SIZE - info->xmit_cnt - 1, SERIAL_XMIT_SIZE - info->xmit_head));
			if (c <= 0)
				break;

			c -= copy_from_user(nuvar_tmp_buf, buf, c);
			if (!c) {
				if (!total)
					total = -EFAULT;
				break;
			}

			spin_lock_irqsave(&info->lock, flags);
			c = MIN(c, MIN(SERIAL_XMIT_SIZE - info->xmit_cnt - 1,
				       SERIAL_XMIT_SIZE - info->xmit_head));
			memcpy(info->xmit_buf + info->xmit_head, nuvar_tmp_buf, c);
			info->xmit_head = (info->xmit_head + c) & (SERIAL_XMIT_SIZE - 1);
			info->xmit_cnt += c;
			spin_unlock_irqrestore(&info->lock, flags);

			buf += c;
			count -= c;
			total += c;
		}
		up(&nuvar_tmp_buf_sem);
	} else {
		spin_lock_irqsave(&info->lock, flags);
		while (1) {
			c = MIN(count, MIN(SERIAL_XMIT_SIZE - info->xmit_cnt - 1,SERIAL_XMIT_SIZE - info->xmit_head));
			if (c <= 0) {
				break;
			}

			memcpy(info->xmit_buf + info->xmit_head, buf, c);
			info->xmit_head = (info->xmit_head + c) & (SERIAL_XMIT_SIZE - 1);
			info->xmit_cnt += c;

			buf += c;
			count -= c;
			total += c;
		}
		spin_unlock_irqrestore(&info->lock, flags);
	}
	return (total);
}
#else
static int nuart_write(struct tty_struct *tty, const unsigned char *buf, int count)
{
	struct nuart_struct *info = (struct nuart_struct *) tty->driver_data;
	int c, total = 0;
	unsigned long flags;

	if (!tty || !info->xmit_buf || !nuvar_tmp_buf)
		return (0);

	spin_lock_irqsave(&info->lock, flags);
	while (1) {
		c = MIN(count, MIN(SERIAL_XMIT_SIZE - info->xmit_cnt - 1,SERIAL_XMIT_SIZE - info->xmit_head));
		if (c <= 0) {
			break;
		}

		memcpy(info->xmit_buf + info->xmit_head, buf, c);
		info->xmit_head = (info->xmit_head + c) & (SERIAL_XMIT_SIZE - 1);
		info->xmit_cnt += c;

		buf += c;
		count -= c;
		total += c;
	}
	spin_unlock_irqrestore(&info->lock, flags);
	return (total);
}
#endif


static void nuart_put_char(struct tty_struct *tty, unsigned char ch)
{
	struct nuart_struct *info = (struct nuart_struct *) tty->driver_data;
	unsigned long flags;

	if (!tty || !info->xmit_buf)
		return;

	spin_lock_irqsave(&info->lock, flags);
	if (info->xmit_cnt >= SERIAL_XMIT_SIZE - 1)
	{
		spin_unlock_irqrestore(&info->lock, flags);
		return;
	}
	info->xmit_buf[info->xmit_head++] = ch;
	info->xmit_head &= SERIAL_XMIT_SIZE - 1;
	info->xmit_cnt++;
	spin_unlock_irqrestore(&info->lock, flags);
}

static void nuart_flush_chars(struct tty_struct *tty)
{
	return;
}

static int nuart_write_room(struct tty_struct *tty)
{
	struct nuart_struct *info = (struct nuart_struct *) tty->driver_data;
	int ret;

	ret = SERIAL_XMIT_SIZE - info->xmit_cnt - 1;
	if (ret < 0)
		ret = 0;
	return (ret);
}

static int nuart_chars_in_buffer(struct tty_struct *tty)
{
	struct nuart_struct *info = (struct nuart_struct *) tty->driver_data;

	return (info->xmit_cnt);
}

static void nuart_flush_buffer(struct tty_struct *tty)
{
	struct nuart_struct *info = (struct nuart_struct *) tty->driver_data;
	unsigned long flags;

	spin_lock_irqsave(&info->lock, flags);
	info->xmit_cnt = info->xmit_head = info->xmit_tail = 0;
	spin_unlock_irqrestore(&info->lock, flags);
	wake_up_interruptible(&tty->write_wait);
	if ((tty->flags & (1 << TTY_DO_WRITE_WAKEUP)) &&
	    tty->ldisc.write_wakeup)
		(tty->ldisc.write_wakeup) (tty);
}

static void nuart_send_break(struct nuart_struct *info, int duration)
{
	unsigned long flags;
	if (!info->base)
		return;
	set_current_state(TASK_INTERRUPTIBLE);

	spin_lock_irqsave(&info->lock, flags);
	nuart_break(info->base, &info->reg, 1);
	spin_unlock_irqrestore(&info->lock, flags);

	schedule_timeout(duration);

	spin_lock_irqsave(&info->lock, flags);
	nuart_break(info->base, &info->reg, 0);
	spin_unlock_irqrestore(&info->lock, flags);
}

static int nuart_tiocmget(struct tty_struct * tty, struct file * file)
{
	struct nuart_struct * info = (struct nuart_struct *)tty->driver_data;
	unsigned long flags, status;
	
	if(PORTNO(tty) == NUART_PORTS)
		return (-ENOIOCTLCMD);
	if(tty->flags & (1 << TTY_IO_ERROR))
		return (-EIO);

	spin_lock_irqsave(&info->lock, flags);
	status = nuart_get_stat(info->base);
	spin_unlock_irqrestore(&info->lock, flags);

	if((status & (NUART_RI | NUART_DSR | NUART_DCD | NUART_CTS)) != 
		(info->reg.mpr & (NUART_RI | NUART_DSR | NUART_DCD | NUART_CTS)))
	    nuart_check_modem_status(info, status);
	info->reg.mpr = status;

	return ((status & NUART_DCD) ? TIOCM_CAR : 0) |
	    ((status & NUART_RI) ? TIOCM_RNG : 0) |
	    ((status & NUART_DSR) ? TIOCM_DSR : 0) |
	    ((status & NUART_CTS) ? TIOCM_CTS : 0) |
	    ((info->reg.pcr & NUART_DTRC_HIGH) ? TIOCM_DTR : 0) |
	    ((info->reg.pcr & NUART_RTSC_HIGH) ? TIOCM_RTS : 0);
}
static int nuart_tiocmset(struct tty_struct *tty, struct file * file, unsigned int set, unsigned int clear)
{
	struct nuart_struct * info = (struct nuart_struct *)tty->driver_data;
	unsigned long flags;
	int dtr = 0, rts = 0;

	if(PORTNO(tty) == NUART_PORTS)
		return (-ENOIOCTLCMD);
	if(tty->flags & (1 << TTY_IO_ERROR))
		return (-EIO);

	if(set & TIOCM_RTS)
		rts = 1;
	if(set & TIOCM_DTR)
		dtr = 1;

	if(clear & TIOCM_RTS)
		rts = 0;
	if(clear & TIOCM_DTR)
		dtr = 0;

	spin_lock_irqsave(&info->lock, flags);
	nuart_dtr(info->base, &info->reg, dtr);
	nuart_rts(info->base, &info->reg, rts);
	spin_unlock_irqrestore(&info->lock, flags);
	return (0);
}
static int nuart_get_serial_info(struct nuart_struct *info,
				 struct serial_struct *retinfo)
{
	struct serial_struct tmp;

	if (!retinfo)
		return (-EFAULT);
	memset(&tmp, 0, sizeof(tmp));
	tmp.line = info->port;
	tmp.port = info->base;
	tmp.flags = info->flags;
	tmp.baud_base = info->baud_base;
	tmp.close_delay = info->close_delay;
	tmp.closing_wait = info->closing_wait;
	tmp.hub6 = 0;
	return copy_to_user(retinfo, &tmp, sizeof(*retinfo)) ? -EFAULT : 0;
}


static int nuart_set_serial_info(struct nuart_struct *info,
				 struct serial_struct *new_info)
{
	struct serial_struct new_serial;
	unsigned int flags;
	int retval = 0;

	if (!new_info || !info->base)
		return (-EFAULT);
	if (copy_from_user(&new_serial, new_info, sizeof(new_serial)))
		return -EFAULT;

	if ((new_serial.port != info->base) ||
	     (new_serial.baud_base != info->baud_base))
		return (-EPERM);

	flags = info->flags & ASYNC_SPD_MASK;

	if (!capable(CAP_SYS_ADMIN)) {
		if ((new_serial.baud_base != info->baud_base) ||
		    (new_serial.close_delay != info->close_delay) ||
		    ((new_serial.flags & ~ASYNC_USR_MASK) !=
		     (info->flags & ~ASYNC_USR_MASK)))
			return (-EPERM);
		info->flags = ((info->flags & ~ASYNC_USR_MASK) |
			       (new_serial.flags & ASYNC_USR_MASK));
	} else {
		/*
		 * OK, past this point, all the error checking has been done.
		 * At this point, we start making changes.....
		 */
		info->flags = ((info->flags & ~ASYNC_FLAGS) |
			       (new_serial.flags & ASYNC_FLAGS));
		info->close_delay = new_serial.close_delay * HZ / 100;
		info->closing_wait = new_serial.closing_wait * HZ / 100;
	}

	if (info->flags & ASYNC_INITIALIZED) {
		if (flags != (info->flags & ASYNC_SPD_MASK)) {
			nuart_change_speed(info, 0);
		}
	} else
		retval = nuart_startup(info);
	return (retval);
}

static int nuart_get_lsr_info(struct nuart_struct *info, unsigned int *value)
{
	unsigned int result;
	unsigned long flags;

	spin_lock_irqsave(&info->lock, flags);
	info->reg.mpr = nuart_get_stat(info->base);
	spin_unlock_irqrestore(&info->lock, flags);

	result = ((info->reg.mpr & NUART_TBUSY) ? 0 : TIOCSER_TEMT);
	return put_user(result, value);
}


static int nuart_ioctl(struct tty_struct *tty, struct file *file,
		       unsigned int cmd, unsigned long arg)
{
	unsigned long flags;
	struct nuart_struct *info = (struct nuart_struct *) tty->driver_data;
	int retval, ret;
	struct async_icount cprev, cnow;	/* kernel counter temps */
	struct serial_icounter_struct *p_cuser;		/* user space */
	unsigned long templ;
	DECLARE_WAITQUEUE(wait, current);

	if (PORTNO(tty) == NUART_PORTS)
		return 0;
	if ((cmd != TIOCGSERIAL) && (cmd != TIOCMIWAIT) &&
	    (cmd != TIOCGICOUNT)) {
		if (tty->flags & (1 << TTY_IO_ERROR))
			return (-EIO);
	}
	switch (cmd) 
	{
	case TCSBRK:		/* SVID version: non-zero arg --> no break */
		retval = tty_check_change(tty);
		if (retval)
			return (retval);
		tty_wait_until_sent(tty, 0);
		if (!arg)
			nuart_send_break(info, HZ / 4);		/* 1/4 second */
		return (0);
	case TCSBRKP:		/* support for POSIX tcsendbreak() */
		retval = tty_check_change(tty);
		if (retval)
			return (retval);
		tty_wait_until_sent(tty, 0);
		nuart_send_break(info, arg ? arg * (HZ / 10) : HZ / 4);
		return (0);
	case TIOCGSOFTCAR:
		return put_user(C_CLOCAL(tty) ? 1 : 0, (unsigned long *) arg);
	case TIOCSSOFTCAR:
		if(get_user(templ, (unsigned long *) arg))
			return -EFAULT;
		arg = templ;
		tty->termios->c_cflag = ((tty->termios->c_cflag & ~CLOCAL) |
					 (arg ? CLOCAL : 0));
		return (0);
//	case TIOCMGET:
//		return (nuart_get_modem_info(info, (unsigned int *) arg));
//	case TIOCMBIS:
//	case TIOCMBIC:
//	case TIOCMSET:
//		return (nuart_set_modem_info(info, cmd, (unsigned int *) arg));
	case TIOCGSERIAL:
		return (nuart_get_serial_info(info, (struct serial_struct *) arg));
	case TIOCSSERIAL:
		return (nuart_set_serial_info(info, (struct serial_struct *) arg));
	case TIOCSERGETLSR:	/* Get line status register */
		return (nuart_get_lsr_info(info, (unsigned int *) arg));
		/*
		 * Wait for any of the 4 modem inputs (DCD,RI,DSR,CTS) to change
		 * - mask passed in arg for lines of interest
		 *   (use |'ed TIOCM_RNG/DSR/CD/CTS for masking)
		 * Caller should use TIOCGICOUNT to see which one it was
		 */
	case TIOCMIWAIT:
		spin_lock_irqsave(&info->lock, flags);
		cprev = info->icount;
		spin_unlock_irqrestore(&info->lock, flags);
		add_wait_queue(&info->delta_msr_wait, &wait);
		while(1)
		{
			spin_lock_irqsave(&info->lock, flags);
			cnow = info->icount;
			spin_unlock_irqrestore(&info->lock, flags);
			set_current_state(TASK_INTERRUPTIBLE);
			if(cnow.rng == cprev.rng && cnow.dsr == cprev.dsr && cnow.dcd == cprev.dcd && cnow.cts == cprev.cts)
			{
				ret = -EIO;
				break;
			}
			if(((arg & TIOCM_RNG) && (cnow.rng != cprev.rng)) ||
			((arg & TIOCM_DSR) && (cnow.dsr != cprev.dsr)) ||
			((arg & TIOCM_CD) && (cnow.dcd != cprev.dcd)) ||
			((arg & TIOCM_CTS) && (cnow.cts != cprev.cts)))
			{
				ret = 0;
				break;
			}
			schedule();
			if(signal_pending(current))
			{
				ret = -ERESTARTSYS;
				break;
			}
			cprev = cnow;
		}
		current->state=TASK_RUNNING;
		remove_wait_queue(&info->delta_msr_wait, &wait);
		return ret;
		/* NOT REACHED */
		break;
	case TIOCGICOUNT:
		spin_lock_irqsave(&info->lock, flags);
		cnow = info->icount;
		spin_unlock_irqrestore(&info->lock, flags);
		p_cuser = (struct serial_icounter_struct *)arg;
		if(put_user(cnow.cts, &p_cuser->cts))
			return -EFAULT;
		if(put_user(cnow.dsr, &p_cuser->dsr))
			return -EFAULT;
		if(put_user(cnow.rng, &p_cuser->rng))
			return -EFAULT;
		return put_user(cnow.dcd, &p_cuser->dcd);
	default:
		return (-ENOIOCTLCMD);
	}
	return (0);
}

static void nuart_throttle(struct tty_struct *tty)
{
}

static void nuart_unthrottle(struct tty_struct *tty)
{
}

static void nuart_set_termios(struct tty_struct *tty,
			      struct termios *old_termios)
{
	struct nuart_struct *info = (struct nuart_struct *) tty->driver_data;

	if ((tty->termios->c_cflag != old_termios->c_cflag) ||
	    (RELEVANT_IFLAG(tty->termios->c_iflag) !=
	     RELEVANT_IFLAG(old_termios->c_iflag))) 
	{

		nuart_change_speed(info, old_termios);

		if ((old_termios->c_cflag & CRTSCTS) && !(tty->termios->c_cflag & CRTSCTS)) {
			tty->hw_stopped = 0;
			nuart_start(tty);
		}
	}
/* Handle sw stopped */
	if ((old_termios->c_iflag & IXON) && !(tty->termios->c_iflag & IXON))
	{
		tty->stopped = 0;
		nuart_start(tty);
	}
}

static void nuart_stop(struct tty_struct *tty)
{
	struct nuart_struct * info  = (struct nuart_struct *)tty->driver_data;
	if(info->xmit_cnt && info->xmit_buf)
		info->lflag &= ~NUART_LFLAG_THRI;
}

static void nuart_start(struct tty_struct *tty)
{
	struct nuart_struct * info  = (struct nuart_struct *)tty->driver_data;
	if(info->xmit_cnt && info->xmit_buf)
		info->lflag |= NUART_LFLAG_THRI;
}

void nuart_hangup(struct tty_struct *tty)
{
	struct nuart_struct *info = (struct nuart_struct *) tty->driver_data;

	nuart_flush_buffer(tty);
	nuart_shutdown(info);
	info->count = 0;
	info->event = 0;
	info->flags &= ~ASYNC_NORMAL_ACTIVE;
	info->tty = 0;
	wake_up_interruptible(&info->open_wait);
}

static void nuart_do_softint(void *private_)
{
	struct nuart_struct *info = (struct nuart_struct *)private_;
	struct tty_struct  *tty;
	
	tty = info->tty;
	if(tty)
	{
		if(test_and_clear_bit(NUART_EVENT_TXLOW, &info->event))
		{
			if((tty->flags & (1 << TTY_DO_WRITE_WAKEUP)) && tty->ldisc.write_wakeup)
				(tty->ldisc.write_wakeup)(tty);
			wake_up_interruptible(&tty->write_wait);
		}
		if(test_and_clear_bit(NUART_EVENT_HANGUP, &info->event))
			tty_hangup(tty);
	}
}

module_init(nuart_module_init);
module_exit(nuart_module_exit);
