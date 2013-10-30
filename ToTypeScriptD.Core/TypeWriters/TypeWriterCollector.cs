﻿using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToTypeScriptD.Core.TypeWriters;

namespace ToTypeScriptD.Core.TypeWriters
{
    public interface ITypeNotFoundErrorHandler
    {
        void Handle(TypeReference typeReference);
    }

    public class TypeWriterCollector
    {
        private ITypeNotFoundErrorHandler typeNotFoundErrorHandler;

        public TypeWriterCollector(ITypeNotFoundErrorHandler typeNotFoundErrorHandler)
        {
            this.typeNotFoundErrorHandler = typeNotFoundErrorHandler;
        }
        public void Collect(IEnumerable<Mono.Cecil.TypeDefinition> tds, TypeCollection typeCollection)
        {
            foreach (var item in tds)
            {
                Collect(item, typeCollection);
            }
        }
        public void Collect(Mono.Cecil.TypeDefinition td, TypeCollection typeCollection)
        {
            if (td.ShouldIgnoreType())
            {
                return;
            }

            // don't duplicate types
            if (typeCollection.Contains(td.FullName))
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            var indentCount = 0;
            ITypeWriter typeWriter = PickTypeWriter(td, indentCount, typeCollection);

            td.Interfaces.Each(item =>
            {
                string lookupName = item.FullName;
                if (item.IsGenericInstance)
                {
                    lookupName = item.GetElementType().FullName;
                }
                var foundType = item.Module.Types.SingleOrDefault(w => w.FullName == lookupName);
                if (foundType == null)
                {
                    typeNotFoundErrorHandler.Handle(item);
                    return;
                }

                var itemWriter = new InterfaceWriter(foundType, indentCount, typeCollection);
                typeCollection.Add(foundType.Namespace, foundType.Name, itemWriter);

            });

            typeCollection.Add(td.Namespace, td.Name, typeWriter);
        }

        ITypeWriter PickTypeWriter(TypeDefinition td, int indentCount, TypeCollection typeCollection)
        {
            if (td.IsEnum)
            {
                return new EnumWriter(td, indentCount, typeCollection);
            }

            if (td.IsInterface)
            {
                return new InterfaceWriter(td, indentCount, typeCollection);
            }

            if (td.IsClass)
            {
                return new ClassWriter(td, indentCount, typeCollection);
            }

            throw new NotImplementedException("Could not get a type to generate for:" + td.FullName);
        }

    }
}