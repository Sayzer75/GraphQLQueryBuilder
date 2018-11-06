# Résumé

GraphQL Query Builder permet de simplifier et cadrer la construction de requêtes GraphQL pour un client .NET Core. Les types gérés sont requêtes **simples** avec/sans paramètres, requêtes **complexes** avec/sans paramètres et requêtes **composées**.

## Requête simple

Une requête dite simple possède une opération, des paramètres (optionnels), une action et des champs en retour.

Exemple

```text
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

Une requête dite complexe possède une opération, des paramètres (optionnels), une action et des champs complexes en retour.

Exemple

```text
query ComplexeOp {
    myRequest {
        field1
        complexField {
            _field1
            _field2
        }
    }
}
```

```csharp
 // Création d'une instance du builder
var queryBuilder = new QueryBuilder("ComplexeOp");

// Construction de la requête
queryBuilder
.WithAction(new QueryActionBuilder("myRequest")
.WithField(new NodeField("field1"))
.WithComplexField(new ComplexNodeField(new NodeField("complexField"), new object[]{
    new NodeField("_field1"),
    new NodeField("_field2")
})))
.Build();
```

## Requête composée

Une requête dite composée possède une opération, des paramètres (optionnels), plusieurs actions et des champs simpes/complexes en retour.

Exemple

```text
query ComposeOp {
    action1: myRequest1 {
        _field1
        _field2
    }
    action2: myRequest2 {
        _field3
        _field4
    }
}
```

```csharp
// Création de action1
var action1Query = new QueryBuilder(isCompose: true)
.WithAction(new QueryActionBuilder("myRequest1")
.WithField(new NodeField("_field1"))
.WithField(new NodeField("_field2")));

// Création de action2
var action2Query = new QueryBuilder(isCompose: true)
.WithAction(new QueryActionBuilder("myRequest2")
.WithField(new NodeField("_field3"))
.WithField(new NodeField("_field4")));

// Création de la requête composée
var composeQueryBuilder = new ComposeQueryBuilder("ComposeOp")
{
    Queries = new System.Collections.Generic.Dictionary<string, QueryBuilder>()
    {
        { "actionQuery1", chart1Query },
        { "actionQuery2", chart2Query }
    }
};

// Construction de la requête
composeQueryBuilder.Build();
```

## Informations  de la requête

Une fois que la requête est construite le contenu texte de la requête est accessible via la propriétée `Query`, les variables via `Variables` et le nom de l'opération via `OperationName`

Exemple

```csharp
// définition et construction de la requête
var myQueryBuilder = new QueryBuilder("[OperationName]");
//...
myQueryBuilder.Build();

// Contenu au format text de la requête construite
string queryText = myQueryBuilder.Query;

// Variables associées à la requête construite
string queryVariables = myQueryBuilder.Variables;

// Nom de l'opération associé à la requête construite
string queryOperationName = myQueryBuilder.OperationName;
```