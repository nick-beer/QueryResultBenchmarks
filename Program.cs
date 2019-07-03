using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NationalInstruments.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace QueryResultBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                WarmupExecuteBenchmarksDirectly();
                ProfileExecuteBenchmarksDirectly();
            }
            else
            {
                BenchmarkRunner.Run<QueryResultBenchmarks>();
            }
        }

        private static void WarmupExecuteBenchmarksDirectly()
        {
            ExecuteBenchmarksDirectly();
            Console.WriteLine("Sleeping for 10 seconds");
            Thread.Sleep(10_000);
        }

        private static void ProfileExecuteBenchmarksDirectly()
        {
            ExecuteBenchmarksDirectly();
            Console.WriteLine("Sleeping for 10 seconds");
            Thread.Sleep(10_000);
        }

        private static void ExecuteBenchmarksDirectly()
        {
            var benchmarks = new QueryResultBenchmarks();
            if (benchmarks.AppendedSingleMatch_Create() != false)
            {
                Environment.FailFast("Invalid impl");
            }
        }
    }

    [MemoryDiagnoser]
    public class QueryResultBenchmarks
    {
        private static readonly QueryResult<string> _singleMatchInFirstResult = CreateSingleMatchInFirstResult();
        private static readonly QueryResult<string> _singleMatchInSecondResult = CreateSingleMatchInSecondResult();
        private static readonly QueryResult<string> _singleMatchInEnumerableResult = CreateSingleMatchInEnumerableResult();
        private static readonly QueryResult<string> _multipleMatches = CreateMultipleMatches();
        private static readonly QueryResult<string> _multipleMatchesIncludingEnumerable = CreateMultipleMatchesIncludingEnumerable();
        private static readonly QueryResult<string> _appendedMatchQueryResult = CreateAppendedMatchQueryResult();
        private static readonly QueryResult<string> _appendedMatchEnumerable = CreateAppendedMatchEnumerable();
        private static readonly QueryResult<string> _fiveMatchesAppended = CreateFourMatchesAppended();
        private static readonly QueryResult<string> _oneHundredMatchesAppended = CreateOneHundredMatchesAppended();
        private static readonly QueryResult<string> _resultWithAggregatorAndAppendedList = CreateResultWithAggregatorAndAppendedList();

        private static QueryResult<string> CreateSingleMatchInFirstResult() => new QueryResult<string>("hello");
        private static QueryResult<string> CreateSingleMatchInSecondResult() => QueryResult<string>.FromPair(null, "hello");
        private static QueryResult<string> CreateSingleMatchInEnumerableResult() => new QueryResult<string>(ToEnumerable("hello"));
        private static QueryResult<string> CreateMultipleMatches() => QueryResult<string>.FromFour("a", "b", "c", "d");
        private static QueryResult<string> CreateMultipleMatchesIncludingEnumerable() => new QueryResult<string>("a", Enumerable.Repeat("b", 3));
        private static QueryResult<string> CreateAppendedMatchQueryResult() => new QueryResult<string>("a").AppendedWith(new QueryResult<string>("b"));
        private static QueryResult<string> CreateAppendedMatchEnumerable() => new QueryResult<string>(Enumerable.Empty<string>()).AppendedWith(new QueryResult<string>("a"));
        private static QueryResult<string> CreateFourMatchesAppended() => new QueryResult<string>("a")
            .AppendedWith(new QueryResult<string>("b"))
            .AppendedWith(new QueryResult<string>("c"))
            .AppendedWith(new QueryResult<string>("d"))
            .AppendedWith(new QueryResult<string>("e"));        
        private static QueryResult<string> CreateOneHundredMatchesAppended()
        {
            var result = new QueryResult<string>("a");
            for (int i = 0; i < 100; i++)
            {
                result = result.AppendedWith(new QueryResult<string>("a"));
            }
            return result;
        }
        private static QueryResult<string> CreateResultWithAggregatorAndAppendedList()
        {
            var resultListForAppend = new List<string>() { "d", "e" };

            ServiceAggregator aggregator = new ServiceAggregator();
            aggregator.AttachService("b");
            aggregator.AttachService("c");
            var fromAggregator = new QueryResult<string>("a", aggregator);

            return fromAggregator.AppendedWith(new QueryResult<string>(resultListForAppend));
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public bool SingleMatchInFirst_Create()
            => CreateSingleMatchInFirstResult() == default;

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public bool SingleMatchInSecond_Create()
            => CreateSingleMatchInSecondResult() == default;

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public bool SingleMatchInEnumerable_Create()
            => CreateSingleMatchInEnumerableResult() == default;

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public bool MultipleMatchesInSlots_Create()
            => CreateMultipleMatches() == default;

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public bool MultipleMatchesInSlotsIncludingEnumerable_Create()
            => CreateMultipleMatchesIncludingEnumerable() == default;

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public bool AppendedSingleMatch_Create()
            => CreateAppendedMatchQueryResult() == default;

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public bool AppendedSingleMatchAsEnumerable_Create()
            => CreateAppendedMatchEnumerable() == default;

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public bool Append4Matches_Create()
            => CreateFourMatchesAppended() == default;

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public bool Append100Matches_Create()
            => CreateOneHundredMatchesAppended() == default;


        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public bool AggregatorAndAppendedList_Create()
            => CreateResultWithAggregatorAndAppendedList() == default;

        [Benchmark]
        public string SingleMatchInFirst_FirstOrDefault()
            => _singleMatchInFirstResult.FirstOrDefault();

        [Benchmark]
        public string SingleMatchInSecond_FirstOrDefault()
            => _singleMatchInSecondResult.FirstOrDefault();

        [Benchmark]
        public string SingleMatchInEnumerable_FirstOrDefault()
            => _singleMatchInEnumerableResult.FirstOrDefault();

        [Benchmark]
        public int MultipleMatchesInSlots_Count()
        {
            int count = 0;
            foreach (var result in _multipleMatches)
            {
                count++;
            }
            return count;
        }

        [Benchmark]
        public int MultipleMatchesIncludingEnumerable_Count()
        {
            int count = 0;
            foreach (var result in _multipleMatchesIncludingEnumerable)
            {
                count++;
            }
            return count;
        }

        [Benchmark]
        public int AppendedMatch_Count()
        {
            int count = 0;
            foreach (var result in _appendedMatchQueryResult)
            {
                count++;
            }
            return count;
        }

        [Benchmark]
        public int AppendedMatchAsEnumerable_Count()
        {
            int count = 0;
            foreach (var result in _appendedMatchEnumerable)
            {
                count++;
            }
            return count;
        }

        [Benchmark]
        public int Append4Matches_Count()
        {
            int count = 0;
            foreach (var result in _fiveMatchesAppended)
            {
                count++;
            }
            return count;
        }

        [Benchmark]
        public int Append100Matches_Count()
        {
            int count = 0;
            foreach (var result in _oneHundredMatchesAppended)
            {
                count++;
            }
            return count;
        }



        [Benchmark]
        public int AggregatorAndAppendedList_Count()
        {
            int count = 0;
            foreach (var result in _resultWithAggregatorAndAppendedList)
            {
                count++;
            }
            return count;
        }

        private static IEnumerable<T> ToEnumerable<T>(T t)
        {
            yield return t;
        }
    }
}
