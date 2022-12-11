using System.Collections.Generic;
using System;

namespace Frightworks;

public static class IEnumerableExtensions
{
	public static int HashCombine<T>( this IEnumerable<T> e, Func<T, decimal> selector )
	{
		int result = 0;

		foreach ( var el in e )
		{
			result = HashCode.Combine( result, selector.Invoke( el ) );
		}

		return result;
	}
}
