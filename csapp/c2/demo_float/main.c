#include <stdio.h>
#include <math.h>

void anay_float(const float *data)
{
    unsigned char *ptr = (unsigned char *)data;
    unsigned char float_data[4];
    int i=0;
    int s=0, exp=0, signif=0;
    double frac=0;

    for(i=0; i<4; i++)
    {
	float_data[i] = ptr[3-i];
    }

    s = (float_data[0]&0x80)>>7;
    for(i=0; i<8; i++)
    {
	int bit=0;
	if(i==7)
	    bit = (float_data[1]&0x80)>>7;
	else
	    bit = ((float_data[0]<<(i+1))&0x80)>>7;

	exp <<=1;
	exp += bit;
    }    
    exp -= 127;	    //E=e-bias;	 e=ek-1..e1e0   bias=2^7-1.这是为了让指数的范围在[-126 +127]之间.
    float_data[1]&=0x7F;
    signif = (((int)float_data[1])<<16) | (((int)float_data[2])<<8) | (((int)float_data[3])<<0);
    signif = signif&( (int)pow(2, 23)-1 );
    for(i=0; i<23; i++)
    {
	int bit = signif & (int)pow(2.0, 22-i);
	if(bit)	
	    bit=1;
	else	
	    bit=0;

	frac = frac + bit*pow(2.0, -(i+1));
    }
    frac += 1;

    printf("----------------------float:%.8f-----------------\n", *data);
    printf("s:%d\n", s);
    printf("exp:%d\n", exp);
    printf("signif:0x%x\n", signif);
    printf("2^exp:%f\n", pow(2.0, exp));
    printf("frac:%.8f\n", frac);
    printf("store:%.8f\n", pow(-1, s)*frac*pow(2.0, exp));
}

int main(void)
{
    float a =-324.23;
    unsigned char *b = (unsigned char *)&a;
    int i=0;

    printf("%.8f\n", a);

    for(i=0; i<4; i++)
	printf("0x%x ", b[3-i]);
    printf("\n");

    anay_float(&a);
    return 0;
}
