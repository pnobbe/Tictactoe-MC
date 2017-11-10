using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public static class Randomizer
    {
        static RandomNumberGenerator rng = new RandomNumberGenerator();


        public static long getLong(long maxValue)
        {
            return rng.Next(maxValue);
        }
    }
}

// Based on https://en.wikipedia.org/wiki/Linear_congruential_generator
class RandomNumberGenerator
{
    private const long m = 4294967296; 
    private const long a = 1664525;
    private const long c = 1013904223;
    private long _last;

    public RandomNumberGenerator()
    {
        _last = DateTime.Now.Ticks % m;
    }

    public RandomNumberGenerator(long seed)
    {
        _last = seed;
    }

    public long Next()
    {
        _last = ((a * _last) + c) % m;

        return _last;
    }

    public long Next(long maxValue)
    {
        return Next() % maxValue;
    }
}

