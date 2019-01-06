CREATE VIEW ReviewSummary
AS
SELECT [Asin],AVG([Sentiment]) AS Sentiment, COUNT(*) Review
  FROM [AmazonReviewContext].[dbo].[AmazonReview]
  WHERE Sentiment > 0
  GROUP BY [Asin]
  HAVING COUNT(*) > 10
  --ORDER BY Sentiment DESC