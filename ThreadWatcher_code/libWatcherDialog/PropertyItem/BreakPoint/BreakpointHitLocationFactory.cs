﻿using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger;
using libWatherDebugger.DocumentContext;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace libWatcherDialog.PropertyItem.BreakPoint
{
    public class BreakpointHitLocationFactory : ItemFactory<BreakpointHitLocation>
    {
        private DebugThread _reference;
        public BreakpointHitLocationFactory(DebugThread reference)
        {
            _reference = reference;
            _productList = null;
        }
        public override int CreateProduct()
        {
            _product = _createProduct();
            return Success;
        }
        protected override int _initFactory()
        {
            return Success;
        }
        protected override BreakpointHitLocation _createProduct()
        {
            CodeInformation Code = _reference.Document.Code;
            BreakpointHitLocation hitloaction = new BreakpointHitLocation();
            hitloaction.FileName = Code.FileName;
            hitloaction.LineNumber = Code.StartPosition;
            return hitloaction;
        }

    }
}
