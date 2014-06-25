using libWatherDebugger.Stack;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace libWatherDebugger.Property
{
    public class DebugPropertyFactory : ItemFactory<DebugProperty>
    {
        private IDebugStackFrame2 _stack;
        private IDebugExpressionContext2 _expressionContext;
        private IDebugExpression2 _expression;
        private IDebugProperty2 _debugProperty;
        private string _queryString;
        public DebugPropertyFactory(DebugStackFrame stack) {
            _stack = stack.Stack;
        }
        public DebugPropertyFactory(IDebugStackFrame2 stack) {
            _stack = stack;
        }
        public override int CreateProduct(object material)
        {
            int result = base.CreateProduct(material);
            _product = _createProduct();
            if (_product != null) _productList.Add(_product);
            return result;
        }
        protected override int _initFactory()
        {
            int result = VSConstants.S_FALSE;
            // Get a context for evaluating expressions.
            
            if (VSConstants.S_OK != _stack.GetExpressionContext(out _expressionContext))
            {
                return result;
            }

            // Parse the expression string.
            string error;
            uint errorCharIndex;
            _queryString = _materials as string;

            if (VSConstants.S_OK != _expressionContext.ParseText( _queryString , 
                (uint)enum_PARSEFLAGS.PARSE_EXPRESSION, 10, out _expression, out error, out errorCharIndex))
            {
                Debug.WriteLine("Failed to parse expression.");
                return result;
            }

            // Evaluate the parsed expression.
            if (VSConstants.S_OK != _expression.EvaluateSync((uint)enum_EVALFLAGS.EVAL_NOSIDEEFFECTS,
                unchecked((uint)Timeout.Infinite), null, out _debugProperty))
            {
                Debug.WriteLine("Failed to evaluate expression.");
                return result;
            }

            return VSConstants.S_OK;
        }
        protected DebugProperty _createProduct()
        {
            DebugProperty property = new DebugProperty();
            property.Expression = _expression;
            property.ExpressionContext = _expressionContext;
           // property.DataSize = Convert.ToUInt32( _materials );
            return property;

        }
    }
}
