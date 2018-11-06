using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quadra.Framework.GraphQL;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GraphQL.Essentials.Tests
{
	/// <summary>
	/// TestQueryBuilder.
	/// </summary>
	[TestClass]
	public class TestQueryBuilder
	{
		/// <summary>
		/// Tests the mutation query with parameter.
		/// </summary>
		[TestMethod]
		[TestCategory("MutationQuery")]
		public void TestMutationQuery_WithParameter()
		{
			//

			const string query = @"mutation Test($id:Int!){
			  TestQuery(id:$id){
					boolField
				}
			}";

			//

			const long paramId = 12;

			//

			var queryBuilder = new MutationBuilder("Test");

			//

			queryBuilder.WithAction(new MutationActionBuilder("TestQuery")
							.WithParameter(new Parameter<long>("id", paramId, "id"))
							.WithField(new NodeField("boolField")))
							.Build();

			//

			string parsedQuery = Regex.Replace(query, "[ \n\r\t]", "");
			string parsedMutationQuery = Regex.Replace(queryBuilder.Query, "[ \n\r\t]", "");

			//

			int compareResult = string.Compare(parsedQuery, parsedMutationQuery, System.StringComparison.InvariantCultureIgnoreCase);

			//

			Assert.IsNotNull(queryBuilder.Query);
			Assert.AreEqual(0, compareResult);
		}

		/// <summary>
		/// Tests the compose query.
		/// </summary>
		[TestMethod]
		[TestCategory("ComposeQuery")]
		public void TestComposeQuery_WithSimpleFields()
		{
			//

			const string query = @"query Test{
myTest1:query1{
field1
field2
}
myTest2:query2{
field1
field2
}
}";
			//

			var firstQuery = new QueryBuilder(isCompose: true)
				.WithAction(new QueryActionBuilder("query1")
				.WithField(new NodeField("field1"))
				.WithField(new NodeField("field2")));

			//

			var secondQuery = new QueryBuilder(isCompose: true)
				.WithAction(new QueryActionBuilder("query2")
				.WithField(new NodeField("field1"))
				.WithField(new NodeField("field2")));

			//

			var composeQueryBuilder = new ComposeQueryBuilder("Test")
			{
				Queries = new Dictionary<string, QueryBuilder>
				{
					{
						"myTest1",
						firstQuery
					},
					{
						"myTest2",
						secondQuery
					}
				}
			};

			//

			composeQueryBuilder.Build();

			//

			string parsedQuery = Regex.Replace(query, "[ \n\r\t]", "");
			string parsedComposeQuery = Regex.Replace(composeQueryBuilder.Query, "[ \n\r\t]", "");

			//

			int compareResult = string.Compare(parsedQuery, parsedComposeQuery, System.StringComparison.InvariantCultureIgnoreCase);

			//

			Assert.IsNotNull(composeQueryBuilder.Query);
			Assert.AreEqual(0, compareResult);
		}

		/// <summary>
		/// Tests the compose query with complex fields.
		/// </summary>
		[TestMethod]
		[TestCategory("ComposeQuery")]
		public void TestComposeQuery_WithComplexFields()
		{
			//

			const string query = @"query Context {
	collab: Collaborateur {
		code
		email
		id
		nom
		prenom
		taches {
			id
			pV1
			pV2
			pV3
			userId
		}
	}
	param: Parametrage{
		categoriePrixKm
		categorieTicketResto
		dateValidationNdF
		dateValidationTP
		id
		prixKm
		prixVente
		prixVente2
		prixVente3
	}
}";
			//

			var collaborateurQuery = new QueryBuilder(isCompose: true)
					.WithAction(new QueryActionBuilder("Collaborateur")
					.WithField(new NodeField("code"))
					.WithField(new NodeField("email"))
					.WithField(new NodeField("id"))
					.WithField(new NodeField("nom"))
					.WithField(new NodeField("prenom"))
					.WithComplexField(new ComplexNodeField(new NodeField("taches"), new[]{
					new NodeField("id"),
					new NodeField("pv1"),
					new NodeField("pV2"),
					new NodeField("pV3"),
					new NodeField("userId")
					})));

			//

			var parametrageQuery = new QueryBuilder(isCompose: true)
					.WithAction(new QueryActionBuilder("Parametrage")
					.WithField(new NodeField("categoriePrixKm"))
					.WithField(new NodeField("categorieTicketResto"))
					.WithField(new NodeField("dateValidationNdF"))
					.WithField(new NodeField("dateValidationTP"))
					.WithField(new NodeField("id"))
					.WithField(new NodeField("prixKm"))
					.WithField(new NodeField("prixVente"))
					.WithField(new NodeField("prixVente2"))
					.WithField(new NodeField("prixVente3")));

			//

			var composeQueryBuilder = new ComposeQueryBuilder("Context")
			{
				Queries = new Dictionary<string, QueryBuilder>
				{
					{
						"collab",
						collaborateurQuery
					},
					{
						"param",
						parametrageQuery
					}
				}
			};

			//

			composeQueryBuilder.Build();

			//

			string parsedQuery = Regex.Replace(query, "[ \n\r\t]", "");
			string parsedComposeQuery = Regex.Replace(composeQueryBuilder.Query, "[ \n\r\t]", "");

			//

			int compareResult = string.Compare(parsedQuery, parsedComposeQuery, System.StringComparison.InvariantCultureIgnoreCase);

			//

			Assert.IsNotNull(composeQueryBuilder.Query);
			Assert.AreEqual(0, compareResult);
		}

		/// <summary>
		/// Tests the compose query with parameters.
		/// </summary>
		[TestMethod]
		[TestCategory("ComposeQuery")]
		public void TestComposeQuery_WithParameters()
		{
			//

			const string query = @"query Dashboard($param1: String!, $param2: String!, $param3: String!, $param4: String!) {
  chart1: TempsPassesParJour(startDate: $param1, endDate: $param2) {
	label
	value
  }
  chart2: TempsPassesParDossier(startDate: $param3, endDate: $param4) {
	label
	value
  }
}";
			//

			var chart1Query = new QueryBuilder(isCompose: true)
					.WithAction(new QueryActionBuilder("TempsPassesParJour")
					.WithParameter(new Parameter<string>("startDate", "01/10/2018", "param1"))
					.WithParameter(new Parameter<string>("endDate", "17/10/2018", "param2"))
					.WithField(new NodeField("label"))
					.WithField(new NodeField("value")));

			//

			var chart2Query = new QueryBuilder(isCompose: true)
					.WithAction(new QueryActionBuilder("TempsPassesParDossier")
					.WithParameter(new Parameter<string>("startDate", "01/10/2018", "param3"))
					.WithParameter(new Parameter<string>("endDate", "17/10/2018", "param4"))
					.WithField(new NodeField("label"))
					.WithField(new NodeField("value")));

			//

			var composeQueryBuilder = new ComposeQueryBuilder("Dashboard")
			{
				Queries = new Dictionary<string, QueryBuilder>
				{
					{
						"chart1",
						chart1Query
					},
					{
						"chart2",
						chart2Query
					}
				}
			};

			//

			composeQueryBuilder.Build();

			//

			string parsedQuery = Regex.Replace(query, "[ \n\r\t]", "");
			string parsedComposeQuery = Regex.Replace(composeQueryBuilder.Query, "[ \n\r\t]", "");

			//

			int compareResult = string.Compare(parsedQuery, parsedComposeQuery, System.StringComparison.InvariantCultureIgnoreCase);

			//

			Assert.IsNotNull(composeQueryBuilder.Query);
			Assert.IsNotNull(composeQueryBuilder.Variables);
			Assert.AreEqual(0, compareResult);
		}
	}
}
