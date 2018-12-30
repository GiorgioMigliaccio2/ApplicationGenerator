using ApplicationGenerator.General.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationGenerator.General.CodeGen.CSharp
{
    public class CSharpFromObjectModel
    {
        public Dictionary<string, string> BaseClasses = new Dictionary<string, string>();
        public CSharpFromObjectModel(string baseNamespace)
        {
            GenerateDependencies(baseNamespace);
        }

        private void GenerateDependencies(string baseNamespace)
        {
            var syntaxFactory = SyntaxFactory.CompilationUnit();

            // Add System using statement: (using System)
            syntaxFactory = syntaxFactory.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(baseNamespace + ".Dependencies")).NormalizeWhitespace();
            var classDeclaration = SyntaxFactory.ClassDeclaration("GeneratedBase");
            classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.InternalKeyword));
            @namespace = @namespace.AddMembers(classDeclaration);

           classDeclaration = classDeclaration.AddMembers(
               AddProperty("Description"),
               AddProperty("Version"),
               );


            syntaxFactory = syntaxFactory.AddMembers(@namespace);

            // Normalize and get code as string.
            var code = syntaxFactory
                .NormalizeWhitespace()
                .ToFullString();
            BaseClasses.Add("GeneratedBase.cs", code);
        }

        private PropertyDeclarationSyntax AddProperty(string propertyName, string parseType = "string", bool getAccessor = true, bool setAccessor = true)
        {
            var propertyDeclaration = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(parseType), propertyName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    getAccessor? SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)): null,
                    setAccessor ? SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)): null);
            return propertyDeclaration;
        }

        public string Generate(ObjectModel model)
        {
            // Create CompilationUnitSyntax
            var syntaxFactory = SyntaxFactory.CompilationUnit();

            // Add System using statement: (using System)
            //todo: Change this base class with one that's placed in a target project and reference the right namespace then!!
            syntaxFactory = syntaxFactory.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")), SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("ApplicationGenerator.General.CodeGen.BaseClasses")));


            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(model.id.Remove(model.id.LastIndexOf('.')))).NormalizeWhitespace();


            //todo: add others based on references

            if (model.type.Equals(ObjectType.Class))
            {
                var classDeclaration = SyntaxFactory.ClassDeclaration(model.name.Trim());
                classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                classDeclaration = classDeclaration.AddBaseListTypes(
                    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("GeneratedBase")));
                //todo: fill in the GeneratedBase members!!
                @namespace = @namespace.AddMembers(classDeclaration);

            }
            else
            {
                var enumDeclaration = SyntaxFactory.EnumDeclaration(model.name.Trim());
                enumDeclaration = enumDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                @namespace = @namespace.AddMembers(enumDeclaration);

            }


            

            syntaxFactory = syntaxFactory.AddMembers(@namespace);

            // Normalize and get code as string.
            var code = syntaxFactory
                .NormalizeWhitespace()
                .ToFullString();

            return code;


        }
    }
}
