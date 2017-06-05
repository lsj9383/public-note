#ifndef __GAUSSIAN_H
#define __GAUSSIAN_H

//高斯分布类
class Gaussian
{
public:
	double miu;
	double sigma;

public:
	Gaussian(void):miu(0), sigma(1){};
	Gaussian(double _miu, double _sigma):miu(_miu), sigma(_sigma){};
	double gen_one(void)
	{
		double val=0;

		for (int i=0; i<12; i++)
			val += __gen_unit();	//0-1的均匀随机数
		val-=6;

		return val*sigma+miu;
	}

private:
	double __gen_unit(void)
	{
		return (double)rand()/RAND_MAX;
	}
};

#endif