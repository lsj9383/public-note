int switch_eg(int x, int n)
{
	int result = x;

	switch( n )
	{
	case 0:
		result += 1;
		break;

	case 1:
		result *= 2;
		break;

	case 2:
		result *= 3;
		break;	
	case 3:
		result += 3;
		break;

	case 400:
		result *= 4;
		break;
	
	default:
		result = 0;
		break;
	}

	return result;
}
