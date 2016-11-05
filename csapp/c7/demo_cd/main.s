	.file	"main.c"
	.text
	.p2align 4,,15
.globl main
	.type	main, @function
main:
	pushl	%ebp
	movl	%esp, %ebp
	andl	$-16, %esp
	call	swap
	xorl	%eax, %eax
	movl	%ebp, %esp
	popl	%ebp
	ret
	.size	main, .-main
.globl buf
	.data
	.align 4
	.type	buf, @object
	.size	buf, 8
buf:
	.long	1
	.long	2
	.ident	"GCC: (GNU) 4.4.7 20120313 (Red Hat 4.4.7-16)"
	.section	.note.GNU-stack,"",@progbits
