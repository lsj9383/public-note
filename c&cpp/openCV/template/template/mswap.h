

template<typename T>
void mswap(T &a, T &b)
{
	T temp;

	temp = a;
	a = b;
	b = temp;
}