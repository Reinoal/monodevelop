// 
// AppleScript.cs
//  
// Author:
//       Michael Hutchinson <mhutchinson@novell.com>
// 
// Copyright (c) 2010 Novell, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;

using Foundation;

namespace MonoDevelop.MacInterop
{
	public static class AppleScript
	{
		public static Dictionary<string, string> Run (string scriptSourceFormat, params object[] args)
		{
			return Run (string.Format (scriptSourceFormat, args));
		}

		// A simplistic method of decoding the descriptors
		// descriptors are made up of 3 parts followed by another descriptor or a null descriptor
		// [1] a 4 character parameter name (eg: 'indx' or 'ID  '
		// [2] a 4 letter object type (eg 'ttab') indicating the type of the object the parameter is on
		// [3] a value
		// [4] descriptor
		//
		// This simplistic method ignores the object type, we don't need it at the moment
		// because the parameter names we are looking for are unique.
		static void DecodeDescriptor (NSAppleEventDescriptor descriptor, Dictionary<string, string> valuePairs)
		{
			string key = null;

			// Descriptor indices start at 1
			for (nint i = 1; i <= descriptor.NumberOfItems; i++) {
				var desc = descriptor.DescriptorAtIndex (i);
				if (desc == null) {
					continue;
				}

				if (!string.IsNullOrEmpty (desc.StringValue)) {
					if (key == null) {
						key = desc.StringValue;
					} else {
						valuePairs [key] = desc.StringValue;
					}
				}

				if (desc.NumberOfItems > 1) {
					DecodeDescriptor (desc, valuePairs);
				}
			}
		}

		public static Dictionary<string, string> Run (string scriptSource)
		{
			var script = new NSAppleScript (scriptSource);
			var result = script.ExecuteAndReturnError (out var errorInfo);

			var dict = new Dictionary<string, string> ();
			DecodeDescriptor (result, dict);

			if (result == null) {
				throw new AppleScriptException (errorInfo);
			}

			return dict;
		}
	}

	public enum OsaError : int //this is a ComponentResult typedef - is it long on int64? Many of these values can be gotten from MacErrors.h
	{
		Success = 0,
		CantCoerce = -1700,	
		MissingParameter = -1701,
		CorruptData = -1702,	
		TypeError = -1703,
		MessageNotUnderstood = -1708,
		Timeout = -1712,
		UndefinedHandler = -1717,
		IllegalIndex	 = -1719,
		IllegalRange	 = -1720,
		ParameterMismatch = -1721,
		IllegalAccess = -1723,
		CantAccess = -1728,
		RecordingIsAlreadyOn = -1732,
		SystemError = -1750,
		InvalidID = -1751,
		BadStorageType = -1752,
		ScriptError = -1753,
		BadSelector = -1754,
		SourceNotAvailable = -1756,
		NoSuchDialect = -1757,
		DataFormatObsolete = -1758,
		DataFormatTooNew = -1759,
		ComponentMismatch = -1761,
		CantOpenComponent = -1762,
		GeneralError	 = -2700,
		DivideByZero	 = -2701,
		NumericOverflow = -2702,
		CantLaunch = -2703,
		AppNotHighLevelEventAware = -2704,
		CorruptTerminology = -2705,
		StackOverflow = -2706,
		InternalTableOverflow = -2707,
		DataBlockTooLarge = -2708,
		CantGetTerminology = -2709,
		CantCreate = -2710,
		SyntaxError = -2740,
		SyntaxTypeError = -2741,
		TokenTooLong	 = -2742,
		DuplicateParameter = -2750,
		DuplicateProperty = -2751,
		DuplicateHandler = -2752,
		UndefinedVariable = -2753,
		InconsistentDeclarations = -2754,
		ControlFlowError = -2755,
		IllegalAssign = -10003,
		CantAssign = -10006,
	}
	
	public class AppleScriptException : Exception
	{
		static class NSAppleScriptError
		{
			public const string Number = "NSAppleScriptErrorNumber";
			public const string Message = "NSAppleScriptErrorMessage";
			public const string BriefMessage = "NSAppleScriptErrorBriefMessage";
			public const string AppName = "NSAppleScriptErrorAppName";
		}

		public AppleScriptException (NSDictionary errorDict)
			: base (GetFullMessage (errorDict))
		{
			ErrorCode = (OsaError)IntFromNSDictionary (errorDict, NSAppleScriptError.Number);
			ErrorMessage = StringFromNSDictionary (errorDict, NSAppleScriptError.Message);
			AppName = StringFromNSDictionary (errorDict, NSAppleScriptError.AppName);
		}

		static string GetFullMessage (NSDictionary errorDict)
		{
			string message = StringFromNSDictionary (errorDict, NSAppleScriptError.BriefMessage);
			return message ?? StringFromNSDictionary (errorDict, NSAppleScriptError.Message);
		}

		static string StringFromNSDictionary (NSDictionary dict, string key)
		{
			if (dict.TryGetValue ((NSString)key, out var errorObject)) {
				if (errorObject is NSString errorString) {
					return errorString;
				}
			}

			return null;
		}

		static int IntFromNSDictionary (NSDictionary dict, string key)
		{
			if (dict.TryGetValue ((NSString)key, out var errorObject)) {
				if (errorObject is NSNumber errorNumber) {
					return errorNumber.Int32Value;
				}
			}

			return -1;
		}

		public OsaError ErrorCode {
			get; private set;
		}

		public string AppName {
			get; private set;
		}

		public string ErrorMessage {
			get; private set;
		}
	}
}

