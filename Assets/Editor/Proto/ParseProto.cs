﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

using UnityEditor;

public class ParseProto{



	private const string protoPath = "ProtoGen/protobuf.proto";
	private const string protoTemp = "Assets/Editor/Proto/ProtocolDefine.temp";
	private const string protoSavePath = "Assets/Scripts/Core/NetWorkManager/ProtocolDefine";

	private static void SaveCSharpProtocolFiles(List<cModule> moduleList)
	{
		if (Directory.Exists (protoSavePath))
			Directory.Delete (protoSavePath, true);
		Directory.CreateDirectory (protoSavePath);

		string tempStr = File.ReadAllText (protoTemp);
		for (int i = 0; i < moduleList.Count; i++) {

			string scriptStr = tempStr;
			scriptStr = scriptStr.Replace ("#ModuleMark#",moduleList[i].moduleCn);
			scriptStr = scriptStr.Replace ("#ModuleName#",moduleList[i].moduleEn);
			scriptStr = scriptStr.Replace ("#ModuleValue#",moduleList[i].moduleId.ToString());

			string commandFormat = "\t\t\t/** #CommandMark# */\n\t\t\tpublic const byte #CommandName# = #CommandValue#;\n\n";
			string commandStr = string.Empty;
			for (int c = 0; c < moduleList [i].commandList.Count; c++) {
				string tmpCommandStr = commandFormat;
				tmpCommandStr = tmpCommandStr.Replace ("#CommandMark#",moduleList [i].commandList[c].commandCn);
				tmpCommandStr = tmpCommandStr.Replace ("#CommandName#",moduleList [i].commandList[c].commandEn);
				tmpCommandStr = tmpCommandStr.Replace ("#CommandValue#",moduleList [i].commandList[c].commandId.ToString());
				commandStr += tmpCommandStr;
			}

			scriptStr = scriptStr.Replace ("#Commands#",commandStr);

			string tmpModuleEn = moduleList [i].moduleEn.Substring (0, 1).ToUpper () + moduleList [i].moduleEn.Substring (1);
			File.WriteAllText (protoSavePath + "/Protocol_" + tmpModuleEn + ".cs",scriptStr);
		}
	}


	private const string NetFacadeSavePath = "Assets/Scripts/Core/NetWorkManager/ModuleNetFacade";

	private void SaveCSharpNetFacadeFiles(List<cModule> moduleList)
	{
		if (!Directory.Exists (NetFacadeSavePath))
			Directory.CreateDirectory (NetFacadeSavePath);

		string tempStr = File.ReadAllText (protoTemp);
		for (int i = 0; i < moduleList.Count; i++) {

			string scriptStr = tempStr;
			scriptStr = scriptStr.Replace ("#ModuleMark#",moduleList[i].moduleCn);
			scriptStr = scriptStr.Replace ("#ModuleName#",moduleList[i].moduleEn);
			scriptStr = scriptStr.Replace ("#ModuleValue#",moduleList[i].moduleId.ToString());

			string commandFormat = "\t\t\t/** #CommandMark# */\n\t\t\tpublic const byte #CommandName# = #CommandValue#;\n\n";
			string commandStr = string.Empty;
			for (int c = 0; c < moduleList [i].commandList.Count; c++) {
				string tmpCommandStr = commandFormat;
				tmpCommandStr = tmpCommandStr.Replace ("#CommandMark#",moduleList [i].commandList[c].commandCn);
				tmpCommandStr = tmpCommandStr.Replace ("#CommandName#",moduleList [i].commandList[c].commandEn);
				tmpCommandStr = tmpCommandStr.Replace ("#CommandValue#",moduleList [i].commandList[c].commandId.ToString());
				commandStr += tmpCommandStr;
			}

			scriptStr = scriptStr.Replace ("#Commands#",commandStr);

			File.WriteAllText (protoSavePath + "/" + moduleList [i].moduleEn + "NetFacade.cs",scriptStr);
		}
	}


	[MenuItem("Tools/ParseProto")]
	private static void Parse()
	{
		List<cModule> moduleList = new List<cModule> ();

		List<string> allLines = new List<string> (File.ReadAllLines (protoPath));
		List<string> message = allLines.FindAll (a => a.Contains ("_s2c"));
		for (int i = 0; i < message.Count; i++) {
			message [i] = message [i].Replace ("message", string.Empty).Replace ("{", string.Empty).Replace ("}", string.Empty).Trim ();
		}


		List<string> contentsModel = allLines.FindAll (a => (a.Contains ("#model_begin#") && a.Contains ("#model_end#")));
		for (int i = 0; i < contentsModel.Count; i++) {
			moduleList.Add (cModule.Create (contentsModel[i]));
		}

		List<string> contentsCommand = allLines.FindAll (a => (a.Contains ("#begin#") && a.Contains ("#end#")));
		for (int i = 0; i < contentsCommand.Count; i++) {
			cCommand command = cCommand.Create (contentsCommand[i]);

			int idx = moduleList.FindIndex(a=>a.moduleId == command.moduleId && a.moduleEn == command.moduleEn);
			if (idx >= 0)
				moduleList [idx].commandList.Add (command);
			else
				Debug.LogError ("Found Module. moduleId:" + command.moduleId +  "moduleEn:" + command.moduleEn);
		}
		SaveCSharpProtocolFiles (moduleList);
		AssetDatabase.Refresh ();
	}

	[MenuItem("Tools/ParseProtoLocalTest")]
	private static void ParseToLocalTest()
	{
		List<cModule> moduleList = new List<cModule> ();

		List<string> allLines = new List<string> (File.ReadAllLines (protoPath));
		List<string> message = allLines.FindAll (a => a.Contains ("_s2c"));
		for (int i = 0; i < message.Count; i++) {
			message [i] = message [i].Replace ("message", string.Empty).Replace ("{", string.Empty).Replace ("}", string.Empty).Trim ();
		}


		List<string> contentsModel = allLines.FindAll (a => (a.Contains ("#model_begin#") && a.Contains ("#model_end#")));
		for (int i = 0; i < contentsModel.Count; i++) {
			moduleList.Add (cModule.Create (contentsModel[i]));
		}

		List<string> contentsCommand = allLines.FindAll (a => (a.Contains ("#begin#") && a.Contains ("#end#")));
		for (int i = 0; i < contentsCommand.Count; i++) {
			cCommand command = cCommand.Create (contentsCommand[i]);

			int idx = moduleList.FindIndex(a=>a.moduleId == command.moduleId && a.moduleEn == command.moduleEn);
			if (idx >= 0)
				moduleList [idx].commandList.Add (command);
			else
				Debug.LogError ("Found Module. moduleId:" + command.moduleId +  "moduleEn:" + command.moduleEn);
		}

		for (int i = 0; i < moduleList.Count; i++) {
			File.WriteAllText("Assets/Resources/TestProtocolInfo/" + moduleList[i].moduleEn + ".txt",JsonUtility.ToJson(moduleList[i]));
		}

		AssetDatabase.Refresh ();
	}
}
