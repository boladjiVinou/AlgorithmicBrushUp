package chapter1;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.OutputStream;
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
	 
	 1.1.18
	 mystery(2,25) -> mystery(4,12)+2
	 mystery(4,12) -> mystery(8,6)
	 mystery(8,6) -> mystery(16,3)
	 mystery(16,3)-> mystery(32,1) +16
	 mystery(32,1) -> mystery(64,0)+32
	 mystery(64,0) -> 0
	 mystery(2,25) = 50
	 
	 mystery(3,11) -> mystery(6, 5) + 3
	 mystery(6,5) -> mystery(12,2)+6
	 mystery(12,2) -> mystery(24,1)
	 mystery(24,1) -> mystery(48,0)+24
	 mystery(3,11) = 33
	 
	 mystery compute a multiplication of a by b
	 
	 Replace + by *
	 
	 mystery(2,25) -> mystery(4,12) + 2
	 mystery(4,12) -> mystery(16, 6)
	 mystery(16,6) -> mystery (256,3)
	 mystery(256,3) -> mystery(65536,1)+256
	 mystery(65536,1) -> 65536
	 mystery(2,25) = 33554432
	 it computes a power b
	 * */
	public static String exR1(int n)
	{
	if (n <= 0) return "";
	return exR1(n-3) + n + exR1(n-2) + n;
	}
	public static int mystery(int a, int b)
	{
	if (b == 0) return 0;
	if (b % 2 == 0) return mystery(a+a, b/2);
	return mystery(a+a, b/2) + a;
	}
	
	public static long basicFibonacci(int N)
	{
		if (N == 0) return 0;
		if (N == 1) return 1;
		return basicFibonacci(N-1) + basicFibonacci(N-2);
	}
	public static long _1_1_19(int N)
	{
		if (N == 0) return 0;
		if (N == 1) return 1;
		long[] tmp = new long[2];
		tmp[0] = 0;
		tmp[1] = 1;
		for(int i = 2; i<=N;i++) 
		{
			long val = tmp[0];
			tmp[0] = tmp[1];
			tmp[1] = val + tmp[1];
		}
		return tmp[1];
	}
	public static double _1_1_20(int n) 
	{
		// ln(m*n) = ln(m)+ln(n)
		if(n<2) 
		{
			return 0;
		}
		return Math.log(n)+_1_1_20(n-1);
	}
	public static void _1_1_21() 
	{
		var output = new ByteArrayOutputStream();
		try 
		{
			System.in.transferTo(output);
			try(output)
			{
				System.out.println("|Name |number#1 |number#2 |result|");
				for(var line: output.toString().split("\n")) 
				{
					var parts = line.split(" ");
					if(parts.length == 3) 
					{
						String result = "";
						var num = Integer.parseInt(parts[1].trim());
						var denom = Integer.parseInt(parts[2].trim());
						result = denom != 0 ? String.format("%.3f", (double) num/denom) : "";
						System.out.println(String.format("%s | %s | %s | %s |",parts[0], parts[1], parts[2], result));
					}
					else 
					{
						System.out.println("");
					}
				}
			}
		}
		catch (IOException e)
		{
			System.out.println("Something went wrong");
		}
	}
	public static void test() 
	{/*
		for(int i = 47; i<50; i++) 
		{
			var expected = basicFibonacci(i);
			var computed = _1_1_19(i);
			if(computed != expected) 
			{
				System.out.println("Wrong value "+i+" expected "+expected+" computed "+computed);
			}
		}*/

		_1_1_21();
	}
}
