enum
{
	NUART_BOARD_RS232 = 0,
	NUART_BOARD_RS485 = 1,
	NUART_BOARD_RS422_RS485 = 2,
	NUART_BOARD_RS232_RS422_RS485 = 3
};

enum
{
	NUART_PORT_RS232 = 0,
	NUART_PORT_RS422 = 1,
	NUART_PORT_RS485F = 2,
	NUART_PORT_RS485H = 3
};

struct nuart_reg
{
	unsigned long	pcr;
	unsigned long 	xpr;
	unsigned long 	br;
	unsigned long	mpr;
};

void nuart_set_pcr(unsigned long addr, struct nuart_reg * reg,  unsigned long pcr);
void nuart_card_disable(unsigned long addr);
void nuart_card_enable(unsigned long addr);
void nuart_break(unsigned long addr, struct nuart_reg * reg, int value);
void nuart_baud(unsigned long addr , struct nuart_reg * reg,unsigned long quot);
//int nuart_dtr_high(unsigned long addr, struct nuart_reg * reg);
//int nuart_rts_high(unsigned long addr, struct nuart_reg * reg);
void nuart_rts(unsigned long addr, struct nuart_reg * reg, int value);
void nuart_dtr(unsigned long addr, struct nuart_reg * reg, int value);
void nuart_port_enable_and_init(unsigned long addr, struct nuart_reg * reg, int mode);
void nuart_port_disable(unsigned long addr, struct nuart_reg * reg);
void nuart_port_initial(unsigned long addr, struct nuart_reg * reg);
void nuart_set_rrp(unsigned long addr, struct nuart_reg * reg, unsigned char rrp);
void nuart_set_twp(unsigned long addr, struct nuart_reg * reg, unsigned char twp);
void nuart_clr_err(unsigned long addr, struct nuart_reg * reg);
unsigned long nuart_get_stat(unsigned long addr);
int nuart_chk_dcd(unsigned long addr, struct nuart_reg * reg);
int nuart_tx_busy(unsigned long addr, struct nuart_reg * reg);
int nuart_ports(unsigned long addr);
void nuart_read_buf(unsigned long addr, unsigned long rrp, unsigned char * rbuf, int len);
void nuart_write_buf(unsigned long addr,  unsigned long twp, unsigned char * tbuf, int len);
void nuart_set_xchar(unsigned long addr, struct nuart_reg * reg, int xon, int oxff);
void nuart_get_config(unsigned long id, int * type, int *port);
