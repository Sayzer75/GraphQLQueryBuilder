using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Quadra.Framework.GraphQL
{
    /// <summary>
    /// GraphId.
    /// </summary>
    public class GraphId
    {
        /// <summary>
        /// The unique identifier
        /// </summary>
        private string _guid;

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public string Guid => _guid;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphId"/> class.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public GraphId(string guid)
        {
            _guid = guid;
        }
    }

    /// <summary>
    /// ICommonBuilder.
    /// </summary>
    internal interface ICommonBuilder
    {
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        string Query { get; set; }
        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        /// <value>
        /// The variables.
        /// </value>
        dynamic Variables { get; set; }
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        void Build();
    }

    /// <summary>
    /// IComposeBuild.
    /// </summary>
    internal interface IComposeBuild
    {
        /// <summary>
        /// Gets or sets the queries.
        /// </summary>
        /// <value>
        /// The queries.
        /// </value>
        Dictionary<string, QueryBuilder> Queries { get; set; }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        void Build();
    }

    /// <summary>
    /// INodeField
    /// </summary>
    internal interface INodeField
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        string Build();
    }

    /// <summary>
    /// GraphQLCommon.
    /// </summary>
    public static class GraphQLCommon
    {
        /// <summary>
        /// The index
        /// </summary>
        private static int index = 1;

        /// <summary>
        /// Gets the type of the scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetScalarType<T>()
        {
            Type type = typeof(T);

            if (type == typeof(string))
            {
                return "String!";
            }
            else if (type == typeof(double) || type == typeof(float))
            {
                return "Float!";
            }
            else if (type == typeof(double?) || type == typeof(float?))
            {
                return "Float";
            }
            else if (type == typeof(int) || type == typeof(long) || type == typeof(byte))
            {
                return "Int!";
            }
            else if (type == typeof(int?) || type == typeof(long?) || type == typeof(byte?))
            {
                return "Int";
            }
            else if (type == typeof(DateTime))
            {
                return "Date!";
            }
            else if (type == typeof(DateTime?))
            {
                return "Date";
            }
            else if (type == typeof(GraphId))
            {
                return "ID!";
            }
            else
            {
                return "String";
            }
        }

        /// <summary>
        /// Gets the type of the scalar.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string GetScalarType(Type type)
        {
            if (type == typeof(string))
            {
                return "String!";
            }
            else if (type == typeof(double) || type == typeof(float))
            {
                return "Float!";
            }
            else if (type == typeof(double?) || type == typeof(float?))
            {
                return "Float";
            }
            else if (type == typeof(int) || type == typeof(long) || type == typeof(byte))
            {
                return "Int!";
            }
            else if (type == typeof(int?) || type == typeof(long?) || type == typeof(byte?))
            {
                return "Int";
            }
            else if (type == typeof(DateTime))
            {
                return "Date!";
            }
            else if (type == typeof(DateTime?))
            {
                return "Date";
            }
            else if (type == typeof(GraphId))
            {
                return "ID!";
            }
            else
            {
                return "String";
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <returns></returns>
        public static string GetToken() => $"param{index++}";
    }

    /// <summary>
    /// ActionBuilder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionBuilder<T>
    {
        /// <summary>
        /// The action alias
        /// </summary>
        private readonly string _actionAlias;

        /// <summary>
        /// The action name
        /// </summary>
        private readonly string _actionName;
        /// <summary>
        /// The fields
        /// </summary>
        private readonly List<object> _fields = new List<object>();
        /// <summary>
        /// The parameters
        /// </summary>
        private readonly List<object> _parameters = new List<object>();
        /// <summary>
        /// The instance
        /// </summary>
        private T _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionBuilder{T}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="alias">The alias.</param>
        internal ActionBuilder(string name, string alias = "")
        {
            //

            _actionName = name;
            _actionAlias = alias;
        }

        /// <summary>
        /// Gets the action alias.
        /// </summary>
        /// <value>
        /// The action alias.
        /// </value>
        internal string ActionAlias => _actionAlias;

        /// <summary>
        /// The action name
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        internal string ActionName => _actionName;
        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        internal List<object> Fields => _fields;

        /// <summary>
        /// Sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        internal T Instance
        {
            set
            {
                _instance = value;
            }
        }

        /// <summary>
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        internal List<object> Parameters => _parameters;

        /// <summary>
        /// Withes the complex field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public T WithComplexField(ComplexNodeField field)
        {
            //

            _fields.Add(field);

            //

            return _instance;
        }

        /// <summary>
        /// Withes the complex parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public T WithComplexParameter(ComplexParameter parameter)
        {
            //

            _parameters.Add(parameter);

            //

            return _instance;
        }

        /// <summary>
        /// Withes the field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public T WithField(NodeField field)
        {
            //

            _fields.Add(field);

            //

            return _instance;

        }

        /// <summary>
        /// Withes the parameter.
        /// </summary>
        /// <typeparam name="B"></typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public T WithParameter<B>(Parameter<B> parameter)
        {
            //

            _parameters.Add(parameter);

            //

            return _instance;
        }
    }

    /// <summary>
    /// BaseParameter.
    /// </summary>
    public class BaseParameter
    {
        /// <summary>
        /// The type
        /// </summary>
        public Type _type;

        /// <summary>
        /// The name
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// The token
        /// </summary>
        private readonly string _token;

        /// <summary>
        /// The value
        /// </summary>
        private readonly object _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseParameter" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <param name="token">The token.</param>
        public BaseParameter(string name, object value, Type type, string token = null)
        {

            _name = name;
            _token = string.IsNullOrEmpty(token) ? GraphQLCommon.GetToken() : token;
            _value = value;
            _type = type;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _name;

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token => _token;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value => _value;
        /// <summary>
        /// Types this instance.
        /// </summary>
        /// <returns></returns>
        public string Type() => GraphQLCommon.GetScalarType(_type);
    }

    /// <summary>
    /// BaseNodeField.
    /// </summary>
    public class BaseNodeField
    {
    }

    /// <summary>
    /// ComplexNodeField.
    /// </summary>
    /// <seealso cref="Quadra.Framework.GraphQL.INodeField" />
    public class ComplexNodeField : INodeField
    {
        /// <summary>
        /// The fields
        /// </summary>
        private readonly object[] _childs;
        /// <summary>
        /// The root
        /// </summary>
        private readonly NodeField _root;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexNodeField" /> class.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="childrens">The childrens.</param>
        public ComplexNodeField(NodeField root, object[] childrens)
        {
            _childs = childrens;
            _root = root;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public string Build()
        {
            //

            var str = "";

            //

            str += _root.Build() + "{\n";

            //

            foreach (var f in _childs)
            {
                //

                str += (f as INodeField)?.Build();
            }

            //

            str += "}";

            //

            return str;
        }
    }

    /// <summary>
    /// ComplexParameter.
    /// </summary>
    public class ComplexParameter
    {
        /// <summary>
        /// The childs
        /// </summary>
        private readonly List<object> _childs;

        /// <summary>
        /// The name
        /// </summary>
        private readonly string _name;
        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexParameter" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="childs">The childs.</param>
        public ComplexParameter(string name, List<object> childs)
        {
            //

            _name = name;

            //

            _childs = childs;
        }

        /// <summary>
        /// Gets the childs.
        /// </summary>
        /// <value>
        /// The childs.
        /// </value>
        public List<object> Childs => _childs;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _name;
    }

    /// <summary>
    /// MutationActionBuilder.
    /// </summary>
    public class MutationActionBuilder : ActionBuilder<MutationActionBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutationActionBuilder" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="alias">The alias.</param>
        public MutationActionBuilder(string name, string alias = "") : base(name, alias)
        {
            //

            base.Instance = this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public string Build()
        {
            //

            var stringBuilder = new StringBuilder();

            //

            stringBuilder.Append(!string.IsNullOrEmpty(ActionAlias) ? $"{ActionAlias}:{ActionName}" : ActionName);

            //

            if (Parameters?.Count > 0)
            {
                //

                var firstParameter = Parameters?.FirstOrDefault();

                //

                if (firstParameter is ComplexParameter)
                {
                    //

                    stringBuilder.Append("(input:{").Append(buildParameters()).Append("})");
                }
                else
                {
                    //

                    stringBuilder.Append("(").Append(buildParameters()).Append(")");
                }
            }

            //

            if (Fields?.Count > 0)
            {
                //

                stringBuilder.Append("{");
                stringBuilder.Append("\n").Append(buildFields());
                stringBuilder.Append("}");
            }

            //

            return stringBuilder.ToString();

            //

            string buildParameters()
            {
                //

                var firstParameter = Parameters.FirstOrDefault();

                //

                if (firstParameter is ComplexParameter complexParameter)
                {
                    //

                    ComplexParameter p = (ComplexParameter)Parameters.FirstOrDefault();

                    //

                    var sb = new StringBuilder();

                    //

                    sb.Append(p.Name).Append(": {");

                    //

                    var l = p.Childs.Select(s =>
                    {
                        //

                        var baseParameter = (s as BaseParameter);

                        //

                        return $"{baseParameter.Name}:${baseParameter.Token}";
                    });

                    //

                    sb.Append(l.Any() ? string.Join(",", l) : "");

                    //

                    sb.Append("}");

                    //

                    return sb.ToString();
                }
                else
                {
                    //

                    var sb = new StringBuilder();

                    //

                    var l = Parameters.Select(s =>
                    {
                        //

                        var baseParameter = (s as BaseParameter);

                        //

                        return $"{baseParameter.Name}:${baseParameter.Token}";
                    });

                    //

                    sb.Append(l.Any() ? string.Join(",", l) : "");

                    //

                    return sb.ToString();
                }
            }

            //

            string buildFields()
            {
                //

                var sb = new StringBuilder();

                //

                foreach (var f in Fields)
                {
                    //

                    sb.Append((f as INodeField)?.Build());
                }

                //

                return sb.ToString();
            }
        }
    }

    /// <summary>
    /// MutationBuilder.
    /// </summary>
    public class MutationBuilder : ICommonBuilder
    {
        /// <summary>
        /// The actions
        /// </summary>
        private readonly Dictionary<string, MutationActionBuilder> _actions = new Dictionary<string, MutationActionBuilder>();

        /// <summary>
        /// The operation name
        /// </summary>
        private readonly string _operationName;

        /// <summary>
        /// Gets the name of the operation.
        /// </summary>
        /// <value>
        /// The name of the operation.
        /// </value>
        public string OperationName => _operationName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MutationBuilder"/> class.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        public MutationBuilder(string operationName = "")
        {
            //

            _operationName = operationName;
        }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        /// <value>
        /// The variables.
        /// </value>
        public dynamic Variables { get; set; }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public void Build()
        {
            //

            var stringBuilder = new StringBuilder();

            //

            stringBuilder.Append("mutation");

            //

            if (!string.IsNullOrEmpty(_operationName))
            {
                //

                stringBuilder.Append(" ").Append(_operationName).Append("(").Append(buildParameters()).Append(")");
            }

            //

            if (_actions.Count > 0)
            {
                //

                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine(buildActions());
                stringBuilder.Append("}");

                //

                Variables = buildVariables();
            }

            //

            Query = stringBuilder.ToString();

            //

            string buildParameters()
            {
                //

                var action = _actions.FirstOrDefault();

                //

                var firstParameter = action.Value?.Parameters.FirstOrDefault();

                //

                if (firstParameter is ComplexParameter complexParameter)
                {
                    //

                    var l = complexParameter.Childs.Select(o =>
                    {
                        //

                        var baseParameter = o as BaseParameter;

                        //

                        return $"${baseParameter.Token}:{baseParameter.Type()}";
                    });

                    //

                    return l.Any() ? string.Join(",", l) : "";
                }
                else
                {
                    //

                    var l = action.Value?.Parameters.Select(o =>
                    {
                        //

                        var baseParameter = o as BaseParameter;

                        //

                        return $"${baseParameter.Token}:{baseParameter.Type()}";
                    });

                    //

                    return l.Any() ? string.Join(",", l) : "";
                }
            }

            //

            string buildActions()
            {
                //

                var sb = new StringBuilder();

                //

                foreach (var a in _actions)
                {
                    //

                    sb.Append(a.Value.Build());
                }

                //
                return sb.ToString();
            }

            //

            dynamic buildVariables()
            {
                //

                dynamic variables = new ExpandoObject();

                //

                var expandoDict = variables as IDictionary<string, object>;

                //

                var action = _actions.FirstOrDefault();

                //

                var firstParameter = action.Value?.Parameters.FirstOrDefault();

                //

                if (firstParameter is ComplexParameter complexParameter)
                {
                    //

                    foreach (var p in complexParameter.Childs)
                    {
                        //

                        var baseParameter = p as BaseParameter;

                        //

                        if (baseParameter.Value is GraphId graphId)
                        {
                            //

                            expandoDict.Add(baseParameter.Token, graphId.Guid);
                        }
                        else
                        {
                            //

                            expandoDict.Add(baseParameter.Token, baseParameter.Value);
                        }
                    }
                }
                else
                {
                    //

                    foreach (var p in action.Value?.Parameters)
                    {
                        //

                        var baseParameter = p as BaseParameter;

                        //

                        if (baseParameter.Value is GraphId graphId)
                        {
                            //

                            expandoDict.Add(baseParameter.Token, graphId.Guid);
                        }
                        else
                        {
                            //

                            expandoDict.Add(baseParameter.Token, baseParameter.Value);
                        }
                    }
                }

                //

                return variables;
            }
        }

        /// <summary>
        /// Withes the action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public MutationBuilder WithAction(MutationActionBuilder action)
        {
            //

            _actions.Add(action.ActionName, action);

            //
            return this;
        }
    }

    /// <summary>
    /// NodeField.
    /// </summary>
    /// <seealso cref="Quadra.Framework.GraphQL.INodeField" />
    public class NodeField : INodeField
    {
        /// <summary>
        /// The field
        /// </summary>
        private readonly string _field;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeField"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        public NodeField(string field)
        {
            _field = field;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public string Build()
        {
            //

            return _field + "\n";
        }
    }

    /// <summary>
    /// Parameter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Parameter<T> : BaseParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="token">The token.</param>
        public Parameter(string name, T value, string token = null) : base(name, value, typeof(T), token)
        {
        }
    }

    /// <summary>
    /// QueryActionBuilder.
    /// </summary>
    public class QueryActionBuilder : ActionBuilder<QueryActionBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryActionBuilder" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="alias">The alias.</param>
        public QueryActionBuilder(string name, string alias = "") : base(name, alias)
        {
            base.Instance = this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public string Build()
        {
            //

            var stringBuilder = new StringBuilder();

            //

            stringBuilder.Append(!string.IsNullOrEmpty(ActionAlias) ? $"{ActionAlias}:{ActionName}" : ActionName);

            //

            if (Parameters?.Count > 0)
            {
                //

                stringBuilder.Append("(").Append(buildParameters()).Append(")");
            }

            //

            if (Fields?.Count > 0)
            {
                //

                stringBuilder.Append("{");
                stringBuilder.Append("\n").Append(buildFields());
                stringBuilder.Append("}");
            }

            //

            return stringBuilder.ToString();

            //

            string buildParameters()
            {
                //

                var l = Parameters.Select(s =>
                {
                    //

                    var baseParameter = (s as BaseParameter);

                    //

                    return $"{baseParameter.Name}:${baseParameter.Token}";
                });

                //

                return l.Any() ? string.Join(",", l) : "";
            }

            //

            string buildFields()
            {
                //

                var sb = new StringBuilder();

                //

                foreach (var f in Fields)
                {
                    //

                    sb.Append((f as INodeField)?.Build());
                }

                //

                return sb.ToString();
            }
        }
    }

    /// <summary>
    /// QueryBuilder.
    /// </summary>
    /// <seealso cref="Quadra.Framework.GraphQL.ICommonBuilder" />
    public class QueryBuilder : ICommonBuilder
    {
        /// <summary>
        /// The actions
        /// </summary>
        private readonly Dictionary<string, QueryActionBuilder> _actions = new Dictionary<string, QueryActionBuilder>();

        /// <summary>
        /// The operation name
        /// </summary>
        private readonly string _operationName;

        /// <summary>
        /// The is compose
        /// </summary>
        private readonly bool _isCompose;

        /// <summary>
        /// Gets the name of the operation.
        /// </summary>
        /// <value>
        /// The name of the operation.
        /// </value>
        public string OperationName => _operationName;

        /// <summary>
        /// Gets a value indicating whether this instance is compose.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is compose; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompose => _isCompose;

        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        internal Dictionary<string, QueryActionBuilder> Actions => _actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder" /> class.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="isCompose">if set to <c>true</c> [is compose].</param>
        public QueryBuilder(string operationName = "", bool isCompose = false)
        {
            _operationName = operationName;
            _isCompose = isCompose;
        }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        /// <value>
        /// The variables.
        /// </value>
        public dynamic Variables { get; set; }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public void Build()
        {
            //

            var stringBuilder = new StringBuilder();

            //

            if (!_isCompose)
            {
                //

                stringBuilder.Append("query");
            }

            //

            if (!string.IsNullOrEmpty(_operationName) && !_isCompose)
            {
                //

                stringBuilder.Append(" ").Append(_operationName).Append("(").Append(buildParameters()).Append(")");
            }

            if (!_isCompose)
            {
                //

                stringBuilder.AppendLine("{");
            }

            //

            if (_actions.Count > 0)
            {
                //

                stringBuilder.AppendLine(buildActions());

                //

                Variables = buildVariables();
            }

            //

            if (!_isCompose)
            {
                stringBuilder.Append("}");
            }

            //

            Query = stringBuilder.ToString();

            //

            string buildParameters()
            {
                //

                var l = new List<string>();

                //

                foreach (var a in _actions)
                {
                    //

                    l.AddRange(a.Value?.Parameters?.Select(s =>
                    {
                        var baseParameter = s as BaseParameter;
                        return $"${baseParameter.Token}:{baseParameter.Type()}";
                    }));
                }

                //

                return l.Count > 0 ? string.Join(",", l) : "";
            }

            //

            string buildActions()
            {
                //

                var sb = new StringBuilder();

                //

                foreach (var a in _actions)
                {
                    //

                    sb.Append(a.Value.Build());
                }

                //
                return sb.ToString();
            }

            //

            dynamic buildVariables()
            {
                //

                dynamic variables = new ExpandoObject();

                //

                var expandoDict = variables as IDictionary<string, object>;

                //

                foreach (var a in _actions)
                {
                    //

                    foreach (var p in a.Value?.Parameters)
                    {
                        //

                        var baseParameter = p as BaseParameter;
                        //

                        //

                        if (baseParameter.Value is GraphId graphId)
                        {

                            //

                            expandoDict.Add(baseParameter.Token, graphId.Guid);
                        }
                        else
                        {
                            //

                            expandoDict.Add(baseParameter.Token, baseParameter.Value);
                        }
                    }
                }

                //

                return variables;
            }
        }

        /// <summary>
        /// Withes the action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public QueryBuilder WithAction(QueryActionBuilder action)
        {
            //

            _actions.Add(action.ActionName, action);

            //
            return this;
        }

        /// <summary>
        /// Determines whether this instance has parameters.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has parameters; otherwise, <c>false</c>.
        /// </returns>
        internal bool HasParameters()
        {
            return this._actions.Any(a => a.Value.Parameters?.Count > 0);
        }
    }

    /// <summary>
    /// ComposeQueryBuilder.
    /// </summary>
    /// <seealso cref="Quadra.Framework.GraphQL.IComposeBuild" />
    public class ComposeQueryBuilder : IComposeBuild
    {
        /// <summary>
        /// The operation name
        /// </summary>
        private readonly string _operationName;

        /// <summary>
        /// The parameters
        /// </summary>
        private readonly List<object> _parameters = new List<object>();

        /// <summary>
        /// Gets the name of the operation.
        /// </summary>
        /// <value>
        /// The name of the operation.
        /// </value>
        public string OperationName => _operationName;

        /// <summary>
        /// Gets or sets the queries.
        /// </summary>
        /// <value>
        /// The queries.
        /// </value>
        public Dictionary<string, QueryBuilder> Queries { get; set; }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        /// <value>
        /// The variables.
        /// </value>
        public dynamic Variables
        {
            get
            {
                //

                dynamic variables = new ExpandoObject();

                //

                var expandoDict = variables as IDictionary<string, object>;

                //

                foreach (var q in Queries)
                {
                    //

                    var tmp = q.Value.Variables as IDictionary<string, object>;

                    //

                    foreach (var t in tmp)
                    {
                        //

                        expandoDict.Add(t.Key, t.Value);
                    }
                }

                //

                return expandoDict;
            }
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        public void Build()
        {
            //

            var stringBuilder = new StringBuilder();

            //

            stringBuilder.Append("query");

            //

            if (!string.IsNullOrEmpty(_operationName))
            {
                //

                stringBuilder.Append(" ").Append(_operationName);
            }

            //

            if (Queries.Any(q => q.Value.HasParameters()))
            {
                //

                stringBuilder.Append("(").Append(buildParameters()).Append(")");
            }

            //

            if (Queries.Count > 0)
            {
                //

                stringBuilder.AppendLine("{");
                stringBuilder.Append(buildQueries());
                stringBuilder.Append("}");
            }

            //

            Query = stringBuilder.ToString();

            //

            string buildParameters()
            {
                //

                List<string> parameters = new List<string>();

                //

                foreach (var q in Queries)
                {
                    //

                    foreach (var a in q.Value.Actions)
                    {
                        //

                        foreach (var p in a.Value.Parameters)
                        {
                            //

                            var baseParameter = (p as BaseParameter);

                            //

                            parameters.Add($"${baseParameter.Token}:{baseParameter.Type()}");
                        }
                    }
                }

                //

                return parameters.Any() ? string.Join(",", parameters) : "";
            }

            //

            string buildQueries()
            {
                //

                var sb = new StringBuilder();

                //

                foreach (var query in Queries)
                {
                    //

                    query.Value.Build();

                    //

                    sb.Append(query.Key).Append(":").Append(query.Value.Query);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComposeQueryBuilder"/> class.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        public ComposeQueryBuilder(string operationName)
        {
            _operationName = operationName;
        }
    }
}
