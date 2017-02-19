#ruby模拟lambda演算的一系列实现

#NUMBER
ZERO	= -> p { -> x { x } }
ONE		= -> p { -> x { p[x] } }
TWO		= -> p { -> x { p[p[x]] } }
THREE	= -> p { -> x { p[p[p[x]]] } }
FOUR	= -> p { -> x { p[p[p[p[x]]]] } }
FIVE	= -> p { -> x { p[p[p[p[p[x]]]]] } }

#BOOL
TRUE	= -> x { -> y { x } }
FALSE	= -> x { -> y { y } }
IF 		= -> b { b }

#IS
IS_ZERO = -> n { n[->x{FALSE}][TRUE] }

#PAIR
PAIR  = -> x { -> y { -> p { p[x][y] } } }
LEFT  = -> p { p[-> x { ->y { x } }] }
RIGHT = -> p { p[-> x { ->y { y } }] }


#OPERATON
INCREMENT	= -> n { -> p { ->x { p[n[p][x]] } }}
SLIDE		= -> p { PAIR[RIGHT[p]][INCREMENT[RIGHT[p]]] }
DECREMENT	= -> n {LEFT[n[SLIDE][PAIR[ZERO][ZERO]]]}
ADD 		= -> m { -> n { n[INCREMENT][m] } }
SUBTRACT	= -> m { -> n { n[DECREMENT][m] } }
MULTIPLY	= -> m { -> n { n[ADD[m]][ZERO]}}
POWER		= -> m { -> n { n[MULTIPLY[m]][ONE]}}
IS_LESS_OR_EQUAL = -> m { -> n { IS_ZERO[SUBTRACT[m][n]] } }
Z = ->f { ->x { f[->y {x[x][y]}] }[->x {f[->y{x[x][y]}]}]}

#MOD = ->m {->n { IF[IS_LESS_OR_EQUAL[n][m]][->x {MOD[SUBTRACT[m][n]][n][x]}][m] }}
MOD = Z[->f {->m {->n { IF[IS_LESS_OR_EQUAL[n][m]][->x {MOD[SUBTRACT[m][n]][n][x]}][m] }}}]

#LIST
EMPTY	= PAIR[TRUE][TRUE]
UNSHIFT	=  -> l { -> x { PAIR[FALSE][PAIR[x][l]] } }
IS_EMPTY= LEFT
FIRST	= -> l { LEFT[RIGHT[l]] }
REST	= -> l { RIGHT[RIGHT[l]] }

#RANGE
RANGE = Z[->f{ ->m { ->n {IF[IS_LESS_OR_EQUAL[m][n]][->x{ UNSHIFT[f[INCREMENT[m]][n]][m][x] }][EMPTY]}}}]

#MAP
FOLD = Z[ ->f { ->l { -> x { -> g { IF[IS_EMPTY[l]][x][->y{ g[f[REST[l]][x][g]][FIRST[l]][y] }] }}}} ]
MAP = ->k { ->f { FOLD[k][EMPTY][->l{->x{UNSHIFT[l][f[x]]}}] }}

#string
DIV = Z[ ->f { ->m { -> n { IF[IS_LESS_OR_EQUAL[n][m]][->x{INCREMENT[f[SUBTRACT[m][n]][n]][x]}][ZERO]}}}]
PUSH= ->l{ ->x { FOLD[l][UNSHIFT[EMPTY][x]][UNSHIFT] } }
TO_DIGITS = Z[->f{->n{PUSH[ IF[IS_LESS_OR_EQUAL[n][DECREMENT[TEN]]][EMPTY][->x{f[DIV[n][TEN]][x]}] ][MOD[n][TEN]]}}]

#Debug
def to_integer(number)
	number[->n{n+1}][0]
end

def to_bool(bool)
	bool[true][false]
end

def to_array(proc)
	array = []
	until to_bool(IS_EMPTY[proc])
		array.push(FIRST[proc])
		proc = REST[proc]
	end
	array
end

def show_array(list)
	to_array(list).map{|p| to_integer(p)}
end

def to_char(c)
	'0123456789BFiuz'.slice(to_integer(c))
end

def to_string(s)
	to_array(s).map{ |c| to_char(c) }.join
end

to_integer(ZERO)
to_integer(ONE)
to_integer(TWO)
to_integer(THREE)
to_bool(TRUE)
to_bool(FALSE)
IF[TRUE]['success']['failed']
IF[FALSE]['success']['failed']
to_bool(IS_ZERO[ZERO])
to_bool(IS_ZERO[ONE])
to_integer(LEFT[PAIR[ONE][TWO]])
to_integer(RIGHT[PAIR[ONE][TWO]])
to_integer(ADD[ONE][TWO])
to_integer(MULTIPLY[THREE][TWO])
to_integer(SUBTRACT[THREE][TWO])
to_bool(IS_LESS_OR_EQUAL[THREE][TWO])
to_bool(IS_LESS_OR_EQUAL[TWO][TWO])
to_bool(IS_LESS_OR_EQUAL[ONE][TWO])
to_integer(MOD[THREE][TWO])
to_integer(MOD[POWER[THREE][THREE]][ADD[THREE][TWO]])
my_list = UNSHIFT[ UNSHIFT[ UNSHIFT[ EMPTY ][ THREE ]][ TWO ]][ ONE ]
to_integer(FIRST[my_list])
show_array(my_list)
show_array(RANGE[ONE][FIVE])
to_integer(FOLD[RANGE[ONE][FIVE]][ZERO][ADD])
to_integer(FOLD[RANGE[ONE][FIVE]][ONE][MULTIPLY])
show_array(MAP[RANGE[ONE][FIVE]][INCREMENT])
to_char(ZED)
to_char(B)
to_string(FIZZBUZZ)
show_array(TO_DIGITS[FIVE])
show_array(TO_DIGITS[POWER[FIVE][THREE]])