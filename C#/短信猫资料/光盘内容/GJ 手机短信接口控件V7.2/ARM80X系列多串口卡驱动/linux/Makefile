##############################################################
ifeq	"$(findstring 2.6, $(shell uname -a))" "2.6"
CC = gcc
all:	module 

install: 
	mkdir -p /lib/modules/$(shell uname -r)/kernel/drivers/char/
	cp -f ./jpt.ko /lib/modules/$(shell uname -r)/kernel/drivers/char/
	mkdir -p /lib/modules/$(shell uname -r)/misc/
	cp -f ./jpt.ko /lib/modules/$(shell uname -r)/misc/
	depmod -a

jpt-objs := jpt_driver.o  jpt_core.a

obj-m += jpt.o  
MODULES_LST := jpt.ko

KDIR	:= /lib/modules/$(shell uname -r)/build
PWD	:= $(shell pwd)

module:
	$(MAKE) -C $(KDIR) M=$(PWD) CC=$(CC) modules 
clean: clean_local
clean_local:
	rm -f *.o
	rm -rf ./.tmp_versions
	rm -f jpt.mod*
	rm -f .jpt*
	rm -f jpt.ko
	

else      #Linux 2.4########################################
echo "Sorry, this driver only support Linux2.6 now!"
endif
