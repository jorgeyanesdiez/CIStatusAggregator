// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "As designed", Scope = "NamespaceAndDescendants", Target = "~N:CIStatusAggregator")]
[assembly: SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "Can't do this on this type", Scope = "type", Target = "~T:CIStatusAggregator.Program")]
[assembly: SuppressMessage("Critical Code Smell", "S927:Parameter names should match base declaration and other partial definitions", Justification = "Discarded on purpose", Scope = "member", Target = "~M:CIStatusAggregator.Services.CIStatusAggregatorService.ExecuteAsync(System.Threading.CancellationToken)~System.Threading.Tasks.Task")]
