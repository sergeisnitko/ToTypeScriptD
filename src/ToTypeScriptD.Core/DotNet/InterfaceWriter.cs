﻿using System.Text;
using ToTypeScriptD.Core.TypeWriters;

namespace ToTypeScriptD.Core.DotNet
{
    public class InterfaceWriter : TypeWriterBase
    {
        public InterfaceWriter(Mono.Cecil.TypeDefinition typeDefinition, int indentCount, TypeCollection typeCollection, DotNetConfig config)
            : base(typeDefinition, indentCount, typeCollection, config)
        {
        }

        public override void Write(StringBuilder sb)
        {
            ++IndentCount;
            base.WriteOutMethodSignatures(sb, "interface", "extends");
        }
    }
}
