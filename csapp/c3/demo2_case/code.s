	.file	"code.c"
	.text
.globl switch_eg
	.type	switch_eg, @function
switch_eg:
.LFB0:
	.cfi_startproc
	leal	(%rdi,%rdi,2), %eax
	cmpl	$2, %esi
	je	.L9
	cmpl	$2, %esi
	jg	.L8
	testl	%esi, %esi
	je	.L3
	cmpl	$1, %esi
	.p2align 4,,3
	jne	.L2
	.p2align 4,,6
	jmp	.L11
.L8:
	cmpl	$3, %esi
	.p2align 4,,5
	je	.L6
	cmpl	$400, %esi
	.p2align 4,,2
	je	.L7
.L2:
	movl	$0, %eax
	ret
.L3:
	leal	1(%rdi), %eax
	ret
.L11:
	leal	(%rdi,%rdi), %eax
	.p2align 4,,2
	ret
.L6:
	leal	3(%rdi), %eax
	.p2align 4,,4
	ret
.L7:
	leal	0(,%rdi,4), %eax
.L9:
	rep
	ret
	.cfi_endproc
.LFE0:
	.size	switch_eg, .-switch_eg
	.ident	"GCC: (GNU) 4.4.7 20120313 (Red Hat 4.4.7-16)"
	.section	.note.GNU-stack,"",@progbits
