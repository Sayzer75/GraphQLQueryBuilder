## C'est quoi GraphQLQueryBuilder ?

GraphQLQueryBuilder permet de simplifier et cadrer la construction de requêtes GraphQL pour un client .NET Core. Les types gérés sont requêtes simples avec/sans paramètres, requêtes complexes avec/sans paramètres et requêtes composées.

## Requête simple

Une requête dite simple possède une opération, des paramètres (optionnels), une action et des champs en retour.

Exemple :

```csharp
/* sans paramètres */
query SimpleOp {
    myRequest {
        field1,
        field2
    }
}
/* avec paramètres */
query SimpleOpWithParams($param1: String!, $param2: String!) {
    myRequest(p1: $param1, p2: $param2) {
        field1,
        field2
    }
}
```

L'appel du builder se fait de cette manière pour les deux requêtes précédentes.

```csharp
 // Création d'une instance du builder
 var queryBuilder = new QueryBuilder("SimpleOp");

// Construction de la requête
queryBuilder.WithAction(new QueryActionBuilder("myRequest")
.WithParameter<string>(new Quadra.Framework.GraphQL.Parameter<string>("p1", request.ClientId,"param1"))
.WithParameter<string>(new Quadra.Framework.GraphQL.Parameter<string>("p2", request.ClientId,"param2"))
.WithField(new NodeField("field1"))
.WithField(new NodeField("field2")))
.Build();
```

## Requête complexe

## Requête composée
