/****** Script for SelectTopNRows command from SSMS  ******/
--SELECT TOP 1000 [Id]
--      ,[OrderName]
--      ,[SinglePackAgeWeight]
--      ,[PackAgeCount]
--      ,[TotalPackAgeWeight]
--      ,[MaoWeight]
--      ,[RealWeight]
--      ,[Price]
--      ,[Amount]
--      ,[Remark]
--      ,[AddTime]
--  FROM [FruitCalc].[dbo].[OrderDetail]


  select sum(maoweight) as '总毛重' from [FruitCalc].[dbo].[OrderDetail]
  select sum(TotalPackAgeWeight) as '总皮重' from [FruitCalc].[dbo].[OrderDetail]
  select sum(PackAgeCount) as '总包装数量' from [FruitCalc].[dbo].[OrderDetail]
  select count(1) as '头数' from [FruitCalc].[dbo].[OrderDetail]
  select sum(realweight) as '总净重' from [FruitCalc].[dbo].[OrderDetail]
  select sum(maoweight-(SinglePackAgeWeight*PackAgeCount)) as '总净重((毛重-(单个包装重量*包装数量))' from [FruitCalc].[dbo].[OrderDetail]
  select sum(realweight*price) as '总金额(净重*单价)' from [FruitCalc].[dbo].[OrderDetail]
  select sum(Amount) as '总金额(小计相加)' from [FruitCalc].[dbo].[OrderDetail]
  select sum((MaoWeight-(SinglePackAgeWeight*PackAgeCount))*price) as '总金额((毛重-(单个包装重量*包装数量))*单价)' from [FruitCalc].[dbo].[OrderDetail]


