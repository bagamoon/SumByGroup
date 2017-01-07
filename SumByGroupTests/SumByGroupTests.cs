using Microsoft.VisualStudio.TestTools.UnitTesting;
using SumByGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpectedObjects;
using FluentAssertions;

namespace SumByGroup.Tests
{
    [TestClass()]
    public class SumByGroupTests
    {
        List<Item> _items = new List<Item>();

        [TestInitialize()]
        public void MyTestInitialize()
        {            
            _items.Add(new Item { Id = 1, Cost = 1, Revenue = 11, SellPrice = 21 });
            _items.Add(new Item { Id = 2, Cost = 2, Revenue = 12, SellPrice = 22 });
            _items.Add(new Item { Id = 3, Cost = 3, Revenue = 13, SellPrice = 23 });
            _items.Add(new Item { Id = 4, Cost = 4, Revenue = 14, SellPrice = 24 });
            _items.Add(new Item { Id = 5, Cost = 5, Revenue = 15, SellPrice = 25 });
            _items.Add(new Item { Id = 6, Cost = 6, Revenue = 16, SellPrice = 26 });
            _items.Add(new Item { Id = 7, Cost = 7, Revenue = 17, SellPrice = 27 });
            _items.Add(new Item { Id = 8, Cost = 8, Revenue = 18, SellPrice = 28 });
            _items.Add(new Item { Id = 9, Cost = 9, Revenue = 19, SellPrice = 29 });
            _items.Add(new Item { Id = 10, Cost =10, Revenue = 20, SellPrice = 30 });
            _items.Add(new Item { Id = 11, Cost = 11, Revenue = 21, SellPrice = 31 });
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            _items.Clear();
        }

        [TestMethod()]
        public void SumTest_Cost_GroupBy_3()
        {
            var expected = new List<int> { 6, 15, 24, 21 };

            var actual = _items.SumByGroup(3, p => p.Cost).ToList();

            actual.ShouldBeEquivalentTo(expected);            
        }

        [TestMethod()]
        public void SumTest_Revenue_GroupBy_4()
        {
            var expected = new List<int> { 50, 66, 60 };

            var actual = _items.SumByGroup(4, p => p.Revenue).ToList();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod()]
        public void SumTest_Cost_Customize_ComputeValue_GroupBy_5()
        {
            var expected = new List<int> { 10, 10, 1 };

            var actual = _items.SumByGroup(5, p => p.Cost % 5).ToList();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod()]
        public void SumTest_SellPrice_Big_GroupCount()
        {
            // 999個一組, 全部加總
            var expected = new List<int> { _items.Sum(p => p.SellPrice) };

            var actual = _items.SumByGroup(999, p => p.SellPrice).ToList();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod()]
        public void SumTest_SellPrice_Small_GroupCount()
        {
            // 1個一組, 不須加總
            var expected = _items.Select(p => p.SellPrice);

            var actual = _items.SumByGroup(1, p => p.SellPrice).ToList();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod()]
        public void SumTest_SellPrice_Negative_GroupCount()
        {            
            // 分組數目錯誤
            Action act = () => _items.SumByGroup(-1, p => p.SellPrice).ToList();
            act.ShouldThrow<ArgumentOutOfRangeException>();                       
        }

    }

    public class Item
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int Revenue { get; set; }
        public int SellPrice { get; set; }

    }
}