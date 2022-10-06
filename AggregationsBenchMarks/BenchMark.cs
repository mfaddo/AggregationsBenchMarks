using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregationsBenchMarks
{
    [MemoryDiagnoser]
    [MarkdownExporter]
    [HtmlExporter]

    [PlainExporter]
    [RPlotExporter]
    public class BenchMark
    {
        Order[] _orders;
        OrderLines[] _orderLines;
        Dictionary<int, OrderLines> _orderLinesDict;

        [Params(/*1, 10, 100, 1_000, */10_000/*, 100_000, 200_000, 300_000, 400_000,500_000*/ )]
        public int ListSize;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _orders = Enumerable.Range(0, ListSize).Select((i) => new Order { Id = i, Number = $"Number ${i}" }).ToArray();
            _orderLines = Enumerable.Range(0, ListSize).Select((i) => new OrderLines { OrderId = i, Total = i }).ToArray();
            _orderLinesDict = _orderLines.ToDictionary(l => l.OrderId);
        }


        [Benchmark]
        public List<OrderAggregate> For_Loop()
        {
            var orderAggregates = new List<OrderAggregate>();
            for (var i = 0; i < _orders.Length; i++)
            {
                var order = _orders[i];
                var lines = _orderLines.SingleOrDefault(line => line.OrderId == order.Id);
                orderAggregates.Add(new OrderAggregate
                {
                    OrderId = order.Id,
                    Number = order.Number,
                    Lines = lines
                });
            }
            return orderAggregates;
        }


        [Benchmark]
        public List<OrderAggregate> ForEach_Loop()
        {
            var orderAggregates = new List<OrderAggregate>();
            foreach (var order in _orders)
            {
                var lines = _orderLines.SingleOrDefault(line => line.OrderId == order.Id);
                orderAggregates.Add(new OrderAggregate
                {
                    OrderId = order.Id,
                    Number = order.Number,
                    Lines = lines
                });
            }
            return orderAggregates;
        }


        [Benchmark]
        public List<OrderAggregate> Select_Lookup()
        {
            return _orders
              .Select(order =>
              {
                  var lines = _orderLines.SingleOrDefault(total => total.OrderId == order.Id);
                  return new OrderAggregate
                  {
                      OrderId = order.Id,
                      Number = order.Number,
                      Lines = lines
                  };
              })
              .ToList();
        }

        [Benchmark]
        public List<OrderAggregate> Join()
        {
            return _orders.Join(
              _orderLines,
              order => order.Id,
              lines => lines.OrderId,
              (order, lines) => new OrderAggregate
              {
                  OrderId = order.Id,
                  Number = order.Number,
                  Lines = lines
              })
              .ToList();
        }

        [Benchmark]
        public List<OrderAggregate> Query_Join()
        {
            return (from order in _orders
                    join lines in _orderLines on order.Id equals lines.OrderId
                    select new OrderAggregate
                    {
                        OrderId = order.Id,
                        Number = order.Number,
                        Lines = lines
                    }).ToList();
        }

        [Benchmark]
        public List<OrderAggregate> Dict_Created()
        {
            var orderDict = _orderLines.ToDictionary(k => k.OrderId);
            return _orders
              .Select(order =>
              {
                  var line = orderDict[order.Id];
                  return new OrderAggregate
                  {
                      OrderId = order.Id,
                      Number = order.Number,
                      Lines = line
                  };
              })
              .ToList();
        }

        [Benchmark]
        public List<OrderAggregate> Dict_Exist()
        {
            return _orders
              .Select(order =>
              {
                  var line = _orderLinesDict[order.Id];
                  return new OrderAggregate
                  {
                      OrderId = order.Id,
                      Number = order.Number,
                      Lines = line
                  };
              })
              .ToList();
        }


        [Benchmark]
        public List<OrderAggregate> Manual()
        {
            var line = new Dictionary<int, OrderLines>(_orderLines.Length);
            foreach (var l in _orderLines)
            {
                line.Add(l.OrderId, l);
            }

            var orderAggregates = new List<OrderAggregate>(_orders.Length);
            foreach (var order in _orders)
            {
                line.TryGetValue(order.Id, out var lineOut);
                orderAggregates.Add(new OrderAggregate
                {
                    OrderId = order.Id,
                    Number = order.Number,
                    Lines = lineOut,
                });
            }
            return orderAggregates;
        }

        public class Order
        {
            public int Id { get; set; }
            public string Number { get; set; }
        }

        public class OrderLines
        {
            public int OrderId { get; set; }
            public int Total { get; set; }
        }

        public class OrderAggregate
        {
            public int OrderId { get; set; }
            public string Number { get; set; }
            public OrderLines Lines { get; set; }
        }


        //Most of the times, LINQ will be a bit slower because it introduces overhead.
        //Do not use LINQ if you care much about performance.
        //Use LINQ because you want shorter better readable and maintainable code.
    }
}
