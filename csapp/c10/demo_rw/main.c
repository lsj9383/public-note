#include <csapp.h>

int main(void)
{
    char c=0;

    while( (Read(STDIN_FILENO, &c, 1))!=0 )
    {
	Write(STDOUT_FILENO, &c, 1);
    }

    return 0;
}
