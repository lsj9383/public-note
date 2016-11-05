	.file	"code.c"
	.text
.globl fun1
	.type	fun1, @function
fun1:
	pushl	%ebp
	movl	%esp, %ebp
	movl	8(%ebp), %eax
	leal	7(%eax,%eax,2), %eax
	popl	%ebp
	ret
	.size	fun1, .-fun1
	.ident	"GCC: (GNU) 4.4.7 20120313 (Red Hat 4.4.7-16)"
	.section	.note.GNU-stack,"",@progbits
