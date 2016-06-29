using System;
using System.Collections;
using System.Threading;
using Microsoft.AnalysisServices.AdomdClient;

namespace MyTest
{
	/// <summary>
	/// Summary description for Test.
	/// </summary>
	public class Test
	{
		public Test()
		{
		}

		static ArrayList _list;
		static ArrayList _cmdList=new ArrayList();
		public static void RunInThread()
		{
			CancelThreads();

			_list=new ArrayList();
			for(int i=0;i<1;i++)
			{
				ThreadStart ts=new ThreadStart(Run);
				Thread t=new Thread(ts);
				t.Start();
				_list.Add(t);
			}
		}

		public static void CancelThreads()
		{
//			if(_list==null)
//				return;
//
//			for(int i=0;i<_list.Count;i++)
//			{
//				Thread t=(Thread)_list[i];
//				t.Abort();
//			}			


			if(_cmdList==null)
				return;
			for(int i=0;i<_cmdList.Count;i++)
			{
				AdomdCommand cmd = _cmdList[i] as AdomdCommand;
				if(cmd!=null)
					try
					{						
						cmd.Connection.Close(true);
					}
					catch(Exception exc)
					{
						exc=null;
					}
			}
		}

		public static void Run()
		{
			// open connection
//			string connStr="Data Source=localhost;Initial Catalog=Foodmart 2000;";		
			string connStr="Data Source=10.3.0.247;Initial Catalog=Adventure Works DW Standard Edition;";
//			string connStr="Data Source=http://localhost/xmla/msxisapi.dll;Initial Catalog=Foodmart 2000;";
			AdomdConnection conn = new AdomdConnection(connStr);			

			AdomdCommand cmd=null;

			// execute mdx
			string queryStr=
//				@"select 
//    {Product.Members} on rows,
//    {{Time.Members}} on columns
//from Sales
//";

				@"WITH SET [Promotions_set] AS '{[Promotions].[All Promotions].[Bag Stuffers],[Promotions].[All Promotions].[Best Savings],[Promotions].[All Promotions].[Big Promo],[Promotions].[All Promotions].[Big Time Discounts],[Promotions].[All Promotions].[Big Time Savings],[Promotions].[All Promotions].[Bye Bye Baby],[Promotions].[All Promotions].[Cash Register Lottery],[Promotions].[All Promotions].[Coupon Spectacular],[Promotions].[All Promotions].[Dimes Off],[Promotions].[All Promotions].[Dollar Cutters],[Promotions].[All Promotions].[Dollar Days],[Promotions].[All Promotions].[Double Down Sale],
[Promotions].[All Promotions].[Double Your Savings],[Promotions].[All Promotions].[Fantastic Discounts],[Promotions].[All Promotions].[Free For All],[Promotions].[All Promotions].[Go For It],[Promotions].[All Promotions].[Green Light Days],[Promotions].[All Promotions].[Green Light Special],[Promotions].[All Promotions].[High Roller Savings],[Promotions].[All Promotions].[I Cant Believe It Sale],[Promotions].[All Promotions].[Money Grabbers],[Promotions].[All Promotions].[Money Savers],[Promotions].[All Promotions].[Mystery Sale],[Promotions].[All Promotions].[No Promotion],[Promotions].[All Promotions].[One Day Sale],[Promotions].[All Promotions].[Pick Your Savings],[Promotions].[All Promotions].[Price Cutters],[Promotions].[All Promotions].[Price Destroyers],[Promotions].[All Promotions].[Price Savers],[Promotions].[All Promotions].[Price Slashers],[Promotions].[All Promotions].[Price Smashers],[Promotions].[All Promotions].[Price Winners],[Promotions].[All Promotions].[Sale Winners],[Promotions].[All Promotions].[Sales Days],[Promotions].[All Promotions].[Sales Galore],[Promotions].[All Promotions].[Save-It Sale],[Promotions].[All Promotions].[Saving Days],[Promotions].[All Promotions].[Savings Galore],[Promotions].[All Promotions].[Shelf Clearing Days],
[Promotions].[All Promotions].[Shelf Emptiers],[Promotions].[All Promotions].[Super Duper Savers],[Promotions].[All Promotions].[Super Savers],[Promotions].[All Promotions].[Super Wallet Savers],[Promotions].[All Promotions].[Three for One],[Promotions].[All Promotions].[Tip Top Savings],[Promotions].[All Promotions].[Two Day Sale],[Promotions].[All Promotions].[Two for One],[Promotions].[All Promotions].[Unbeatable Price Savers],[Promotions].[All Promotions].[Wallet Savers],[Promotions].[All Promotions].[Weekend Markdown],[Promotions].[All Promotions].[You Save Days],[Promotions].[All Promotions]}' 
SET [Promotions_set_wcalc] AS '{{[Promotions_set]}}' SET [Time_set] AS '{[*SET Time.1997.Children*],[*SET Time.Q1.Children*],[*SET Time.Q2.Children*],[*SET Time.Q3.Children*],[*SET Time.Q4.Children*]}' SET [*SET Time.Q4.Children*] AS '{[Time].[1997].[Q4].Children}' SET [*SET Time.Q3.Children*] AS '{[Time].[1997].[Q3].Children}' SET [*SET Time.Q2.Children*] AS '{[Time].[1997].[Q2].Children}' SET [*SET Time.Q1.Children*] AS '{[Time].[1997].[Q1].Children}' SET [*SET Time.1997.Children*] AS '{[Time].[1997].Children}' SET [Time_set_wcalc] AS '{{[Time_set]}}' SET [Store Size in SQFT_set] AS '{[*SET Store Size in SQFT.All Store Size in SQFT.Children*]}' SET [*SET Store Size in SQFT.All Store Size in SQFT.Children*] AS '{[Store Size in SQFT].[All Store Size in SQFT].Children}' SET [Store Size in SQFT_set_wcalc] AS '{{[Store Size in SQFT_set]}}' SET [Product_set] AS '{[*SET Product.Drink.Children*],[*SET Product.Food.Children*],[*SET Product.Alcoholic Beverages.Children*],[*SET Product.Beverages.Children*],[*SET Product.Dairy.Children*],[*SET Product.Baked Goods.Children*],
[*SET Product.Baking Goods.Children*],[*SET Product.Breakfast Foods.Children*],[*SET Product.Canned Foods.Children*],[*SET Product.Canned Products.Children*],[*SET Product.Deli.Children*],[*SET Product.Eggs.Children*],[*SET Product.Frozen Foods.Children*],[*SET Product.Meat.Children*],[*SET Product.Produce.Children*],[*SET Product.Seafood.Children*],[*SET Product.Snack Foods.Children*],[*SET Product.Snacks.Children*],[*SET Product.Starchy Foods.Children*],[*SET Product.Non-Consumable.Children*],[*SET Product.Carousel.Children*],[*SET Product.Checkout.Children*],[*SET Product.Health and Hygiene.Children*],[*SET Product.Household.Children*],[*SET Product.Periodicals.Children*]}' SET [*SET Product.Periodicals.Children*] AS '{[Product].[All Products].[Non-Consumable].[Periodicals].Children}' SET [*SET Product.Household.Children*] AS '{[Product].[All Products].[Non-Consumable].[Household].Children}' SET [*SET Product.Health and Hygiene.Children*] AS '{[Product].[All Products].[Non-Consumable].[Health and Hygiene].Children}' SET [*SET Product.Checkout.Children*] AS '{[Product].[All Products].[Non-Consumable].[Checkout].Children}' 
SET [*SET Product.Carousel.Children*] AS '{[Product].[All Products].[Non-Consumable].[Carousel].Children}' SET [*SET Product.Non-Consumable.Children*] AS '{[Product].[All Products].[Non-Consumable].Children}' SET [*SET Product.Starchy Foods.Children*] AS '{[Product].[All Products].[Food].[Starchy Foods].Children}' SET [*SET Product.Snacks.Children*] AS '{[Product].[All Products].[Food].[Snacks].Children}' SET [*SET Product.Snack Foods.Children*] AS '{[Product].[All Products].[Food].[Snack Foods].Children}' SET [*SET Product.Seafood.Children*] AS '{[Product].[All Products].[Food].[Seafood].Children}' SET [*SET Product.Produce.Children*] AS '{[Product].[All Products].[Food].[Produce].Children}' SET [*SET Product.Meat.Children*] AS '{[Product].[All Products].[Food].[Meat].Children}' SET [*SET Product.Frozen Foods.Children*] AS '{[Product].[All Products].[Food].[Frozen Foods].Children}' SET [*SET Product.Eggs.Children*] AS '{[Product].[All Products].[Food].[Eggs].Children}' SET [*SET Product.Deli.Children*] AS '{[Product].[All Products].[Food].[Deli].Children}' SET [*SET Product.Canned Products.Children*] AS '{[Product].[All Products].[Food].[Canned Products].Children}' SET [*SET Product.Canned Foods.Children*] AS '{[Product].[All Products].[Food].[Canned Foods].Children}' 
SET [*SET Product.Breakfast Foods.Children*] AS '{[Product].[All Products].[Food].[Breakfast Foods].Children}' SET [*SET Product.Baking Goods.Children*] AS '{[Product].[All Products].[Food].[Baking Goods].Children}' SET [*SET Product.Baked Goods.Children*] AS '{[Product].[All Products].[Food].[Baked Goods].Children}' SET [*SET Product.Dairy.Children*] AS '{[Product].[All Products].[Food].[Dairy].Children}' SET [*SET Product.Beverages.Children*] AS '{[Product].[All Products].[Drink].[Beverages].Children}' SET [*SET Product.Alcoholic Beverages.Children*] AS '{[Product].[All Products].[Drink].[Alcoholic Beverages].Children}' SET [*SET Product.Food.Children*] AS '{[Product].[All Products].[Food].Children}' SET [*SET Product.Drink.Children*] AS '{[Product].[All Products].[Drink].Children}' 
SET [Product_set_wcalc] AS '{{[Product_set]}}' MEMBER [Promotion Media].[*AGGREGATE*] AS 'AGGREGATE({[Promotion Media].[All Media].[Bulk Mail],[Promotion Media].[All Media].[Cash Register Handout],[Promotion Media].[All Media].[Daily Paper],[Promotion Media].[All Media].[Daily Paper, Radio],[Promotion Media].[All Media].[Daily Paper, Radio, TV],[Promotion Media].[All Media].[In-Store Coupon],[Promotion Media].[All Media].[No Media],[Promotion Media].[All Media].[Product Attachment],[Promotion Media].[All Media].[Radio],[Promotion Media].[All Media].[Street Handout],[Promotion Media].[All Media].[Sunday Paper],[Promotion Media].[All Media].[Sunday Paper, Radio],[Promotion Media].[All Media].[Sunday Paper, Radio, TV],[Promotion Media].[All Media].[TV]})' , SOLVE_ORDER=-100 
MEMBER [Store Type].[*AGGREGATE*] AS 'AGGREGATE({[Store Type].[All Store Type].[Deluxe Supermarket],[Store Type].[All Store Type].[Gourmet Supermarket],[Store Type].[All Store Type].[HeadQuarters],[Store Type].[All Store Type].[Mid-Size Grocery],[Store Type].[All Store Type].[Small Grocery],[Store Type].[All Store Type].[Supermarket]})' , SOLVE_ORDER=-100 MEMBER [Yearly Income].[*AGGREGATE*] AS 'AGGREGATE({[Yearly Income].[All Yearly Income].[$10K - $30K],[Yearly Income].[All Yearly Income].[$110K - $130K],[Yearly Income].[All Yearly Income].[$130K - $150K],[Yearly Income].[All Yearly Income].[$150K +],[Yearly Income].[All Yearly Income].[$30K - $50K],[Yearly Income].[All Yearly Income].[$50K - $70K],[Yearly Income].[All Yearly Income].[$70K - $90K],[Yearly Income].[All Yearly Income].[$90K - $110K]})' , SOLVE_ORDER=-100  SELECT   NON EMPTY  HIERARCHIZE({{[Promotions_set_wcalc]}*{[Time_set_wcalc]}*{[Store Size in SQFT_set_wcalc]}}) ON Columns,  NON EMPTY  HIERARCHIZE({{[Product_set_wcalc]}}) ON Rows  FROM [Sales]  WHERE ([Customers].[All Customers],[Education Level].[All Education Level],[Gender].[All Gender],[Marital Status].[All Marital Status],[Measures].[Unit Sales],[Promotion Media].[*AGGREGATE*],[Store].[All Stores],[Store Type].[*AGGREGATE*],[Yearly Income].[*AGGREGATE*])";

			try
			{
				conn.Open();				

				cmd=conn.CreateCommand();
				_cmdList.Add(cmd);
				cmd.CommandText=queryStr;
				CellSet cst=cmd.ExecuteCellSet();
				cst=null;
			}
			catch(Exception exc)
			{
				throw exc;
			}
			finally
			{
				if(cmd!=null)
					_cmdList.Remove(cmd);
				
				conn.Close();
				conn.Dispose();
			}


		}
	}
}
