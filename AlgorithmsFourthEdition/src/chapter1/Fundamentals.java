package chapter1;

import java.lang.reflect.Array;
import java.util.stream.Stream;

public class Fundamentals
{
	/*
	1.1.1
	 	a) (0+15)/2 = 7
	 	b) 2.0e-6 * 100000000.1 = 200 + 2.0e-7
	 	c) true && false || true && true = true
	1.1.2
		a) (1 + 2.236)/2 = 3.236 / 2 = 1.618 (float)
		b) 1 + 2 + 3 + 4.0 = 10.0 (float)
		c) 4.1 >= 4 = true (boolean)
		d) 1 + 2 + "3" = "33" (string)
	
	 */
	public static void _1_1_3(String[] args) 
	{
		var numbers = Stream.of(args).map(arg -> Integer.parseInt(arg)).limit(3).toList();
		System.out.println(numbers.stream().allMatch(val -> val == numbers.get(0)));
	}
	/*
	 * 	1.1.4
		a) if (a > b) then c = 0; Wrong should not specify then
		b) if a > b { c = 0; } Nothing wrong
		c. if (a > b) c = 0; Nothing wrong
		d. if (a > b) c = 0 else b Wrong b  is not an instruction
	 * */
	public static boolean _1_1_5(double x, double y) 
	{
		return  x > 0 && x<1 && y>0 && y<1;
	}
	/*
	 * 1.1.6
	    0112358   (13)(21)(34)(55)(89) (144)(233)(610)
	    112358(13)(21)(34)(55)(89)(144)(233)(377)()
	    0112358   (13)(21)(34)(55)(89) (144)(233)
	    output: 
	    0
	    1
	    1
	    2
	    3
	    5
	    8
		13
		21
		34
		55
		89
		144
		233
		377
		610
	  1.1.7
		a) 3.00009
		t:9, 5 , 3.4, 3.0235, 3.00009
		b) 499500
		c) compile error
	  1.1.8
		a)b
		b)bc
		c)e
	 * */
	public static String _1_1_9(int n) 
	{
		StringBuilder sb = new StringBuilder();
		for(int i = n; i>0; i/=2) 
		{
			if(i%2 != 0) 
			{
				sb.insert(0,'1');
			}
			else 
			{
				sb.insert(0,'0');
			}
		}
		
		return sb.toString();
	}
	/*
	 * 
	1.1.10
		The array a is not initialized, we will have a compilation error
	 * */
	public static void _1_1_11(boolean[][] values) 
	{
		System.out.println(String.format("%d,%d", values.length, values.length > 0 ? values[0].length: 0));
		for(int i =0 ; i< values.length;i++) 
		{
			for(int j = 0; j< values[i].length; j++) 
			{
				System.out.print(values[i][j] ? '*': ' ');
			}
			System.out.println("");
		}
	}
	public static void _1_1_11_test() 
	{
		var tmp = new boolean[2][];
		for(int i = 0; i<tmp.length;i++) 
		{
			tmp[i] = new boolean[10];
			for(int j = 0; j<tmp[i].length;j++) 
			{
				tmp[i][j] = Math.random() > 0.5;
			}
		}
		_1_1_11(tmp);
	}
	/*
	 * 1.1.12
	 * 9 8 7 6 5 4 3 2 1 0
	 * output: 0 1 2 3 4 5 6 7 8 9
	 * 
	 * */
	public static void _1_1_13(int[][] matrix) 
	{
		if(matrix.length ==0) 
		{
			return;
		}
		for(int i = 0; i< matrix[0].length;i++) 
		{
			for(int j = 0; j< matrix.length;j++) 
			{
				System.out.print(matrix[j][i]);
			}
			System.out.println();
		}
	}

	public static int _1_1_14(int n) 
	{
		int count = 0;
		for(int i = 2; i<n ; i*=2) // 2 because base 2, lg(n) = b => base ^ b = n
		{
			++count;
		}
		return count;
	}
	public static int[] _1_1_15(int[] values, int m) 
	{
		int[] res = new int[m];
		for(int v:values) 
		{
			if(v <m) 
			{
				++res[v];
			}
		}
		return res;
	}
	/*
	 * 1.1.16
	 * 
	 * f(6) = f(3) + 6 + f(4) + 6=311361142246
	 * f(3)= f(0)+3 + f(1)+3 = 3113
	 * f(4) = f(1)+4 + f(2)+4=114224
	 * f(2)=f(-1)+2+f(0)+2 = 22
	 * f(1) = f(-2)+1+f(-2)+1=11
	 * f(6) = "311361142246"
	 * 
	 * 
	 1.1.17
	 this function will lead to stack overflow because we execute the next recurvise methode with a negative number without breaking early
	 * */
	public static String exR1(int n)
	{
	if (n <= 0) return "";
	return exR1(n-3) + n + exR1(n-2) + n;
	}
	public static void test() 
	{

		System.out.println(exR1(6));
	}
}
