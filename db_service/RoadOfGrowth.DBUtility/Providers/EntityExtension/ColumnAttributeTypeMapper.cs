﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RoadOfGrowth.DBUtility.Providers.EntityExtension
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
    }

    public class FallbackTypeMapper : SqlMapper.ITypeMap
    {
        private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

        public FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
        {
            _mappers = mappers;
        }

        public ConstructorInfo FindConstructor(string[] names, Type[] types)
        {
            foreach (var mapper in _mappers)
            {
                ConstructorInfo result = mapper.FindConstructor(names, types);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public ConstructorInfo FindExplicitConstructor()
        {
            return _mappers
                .Select(mapper => mapper.FindExplicitConstructor())
                .FirstOrDefault(result => result != null);
        }

        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            foreach (var mapper in _mappers)
            {
                var result = mapper.GetConstructorParameter(constructor, columnName);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            foreach (var mapper in _mappers)
            {
                var result = mapper.GetMember(columnName);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }

    public class ColumnAttributeTypeMapper : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper(Type objType)
            : base(new SqlMapper.ITypeMap[]
                {
                    new CustomPropertyTypeMap(
                        objType,
                       //typeof(T),
                       (type, columnName) =>
                           type.GetProperties().FirstOrDefault(prop =>
                               prop.GetCustomAttributes(false)
                                   .OfType<ColumnAttribute>()
                                   .Any(attr => attr.Name == columnName)
                               )
                       ),
                    new DefaultTypeMap(objType)
                })
        {
        }
    }

}
