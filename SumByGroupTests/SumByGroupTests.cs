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
        private List<Product> _products = new List<Product>();

        [TestInitialize()]
        public void MyTestInitialize()
        {            
            _products.Add(new Product { Id = 1, Cost = 1, Revenue = 11, SellPrice = 21 });
            _products.Add(new Product { Id = 2, Cost = 2, Revenue = 12, SellPrice = 22 });
            _products.Add(new Product { Id = 3, Cost = 3, Revenue = 13, SellPrice = 23 });
            _products.Add(new Product { Id = 4, Cost = 4, Revenue = 14, SellPrice = 24 });
            _products.Add(new Product { Id = 5, Cost = 5, Revenue = 15, SellPrice = 25 });
            _products.Add(new Product { Id = 6, Cost = 6, Revenue = 16, SellPrice = 26 });
            _products.Add(new Product { Id = 7, Cost = 7, Revenue = 17, SellPrice = 27 });
            _products.Add(new Product { Id = 8, Cost = 8, Revenue = 18, SellPrice = 28 });
            _products.Add(new Product { Id = 9, Cost = 9, Revenue = 19, SellPrice = 29 });
            _products.Add(new Product { Id = 10, Cost = 10, Revenue = 20, SellPrice = 30 });
            _products.Add(new Product { Id = 11, Cost = 11, Revenue = 21, SellPrice = 31 });
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            _products.Clear();
        }

        [TestMethod()]
        public void Sum_Cost_GroupBy_3_Should_Be_6_15_24_21()
        {
            var expected = new List<int> { 6, 15, 24, 21 };

            var actual = _products.SumByGroup(3, p => p.Cost).ToList();

            actual.ShouldBeEquivalentTo(expected);            
        }

        [TestMethod()]
        public void Sum_Revenue_GroupBy_4_Should_Be_50_66_60()
        {
            var expected = new List<int> { 50, 66, 60 };

            var actual = _products.SumByGroup(4, p => p.Revenue).ToList();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod()]
        public void Sum_Cost_Remainder_After_Divided_5_GroupBy_5_Should_Be_10_10_1()
        {
            var expected = new List<int> { 10, 10, 1 };

            var actual = _products.SumByGroup(5, p => p.Cost % 5).ToList();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod()]
        public void Sum_SellPrice_GroupBy_999_Should_Be_Sum_Of_All()
        {
            // 999個一組, 全部加總
            var expected = new List<int> { _products.Sum(p => p.SellPrice) };

            var actual = _products.SumByGroup(999, p => p.SellPrice).ToList();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod()]
        public void Sum_SellPrice_Group_By_1_Should_Be_All_Elements()
        {
            // 1個一組, 不須加總
            var expected = _products.Select(p => p.SellPrice);

            var actual = _products.SumByGroup(1, p => p.SellPrice).ToList();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod()]
        public void Sum_Empty_Source_Should_Be_Empty_Result()
        {            
            _products.Clear();
            var expected = _products;

            var actual = _products.SumByGroup(5, p => p.SellPrice).ToList();

            actual.ShouldBeEquivalentTo(expected);
        }


        [TestMethod()]
        public void Sum_SellPrice_Negative_GroupCount_Should_Throw_ArgumentOutOfRangeException()
        {            
            // 分組數目錯誤
            Action act = () => _products.SumByGroup(-1, p => p.SellPrice).ToList();
            act.ShouldThrow<ArgumentOutOfRangeException>();                       
        }

    }

    public class Product
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int Revenue { get; set; }
        public int SellPrice { get; set; }

    }
}