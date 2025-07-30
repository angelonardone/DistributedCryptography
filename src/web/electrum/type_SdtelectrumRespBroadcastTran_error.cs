/*
				   File: type_SdtelectrumRespBroadcastTran_error
			Description: error
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186702
		   Program type: Callable routine
			  Main DBMS: 
*/
using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using GeneXus.Http.Server;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

using GeneXus.Programs;
namespace GeneXus.Programs.electrum
{
	[XmlRoot(ElementName="electrumRespBroadcastTran.error")]
	[XmlType(TypeName="electrumRespBroadcastTran.error" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtelectrumRespBroadcastTran_error : GxUserType
	{
		public SdtelectrumRespBroadcastTran_error( )
		{
			/* Constructor for serialization */
			gxTv_SdtelectrumRespBroadcastTran_error_Message = "";

		}

		public SdtelectrumRespBroadcastTran_error(IGxContext context)
		{
			this.context = context;	
			initialize();
		}

		#region Json
		private static Hashtable mapper;
		public override string JsonMap(string value)
		{
			if (mapper == null)
			{
				mapper = new Hashtable();
			}
			return (string)mapper[value]; ;
		}

		public override void ToJSON()
		{
			ToJSON(true) ;
			return;
		}

		public override void ToJSON(bool includeState)
		{
			AddObjectProperty("code", gxTpr_Code, false);


			AddObjectProperty("message", gxTpr_Message, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="code")]
		[XmlElement(ElementName="code")]
		public short gxTpr_Code
		{
			get {
				return gxTv_SdtelectrumRespBroadcastTran_error_Code; 
			}
			set {
				gxTv_SdtelectrumRespBroadcastTran_error_Code = value;
				SetDirty("Code");
			}
		}




		[SoapElement(ElementName="message")]
		[XmlElement(ElementName="message")]
		public string gxTpr_Message
		{
			get {
				return gxTv_SdtelectrumRespBroadcastTran_error_Message; 
			}
			set {
				gxTv_SdtelectrumRespBroadcastTran_error_Message = value;
				SetDirty("Message");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Static Type Properties

		[XmlIgnore]
		private static GXTypeInfo _typeProps;
		protected override GXTypeInfo TypeInfo { get { return _typeProps; } set { _typeProps = value; } }

		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtelectrumRespBroadcastTran_error_Message = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected short gxTv_SdtelectrumRespBroadcastTran_error_Code;
		 

		protected string gxTv_SdtelectrumRespBroadcastTran_error_Message;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"electrumRespBroadcastTran.error", Namespace="distributedcryptography")]
	public class SdtelectrumRespBroadcastTran_error_RESTInterface : GxGenericCollectionItem<SdtelectrumRespBroadcastTran_error>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtelectrumRespBroadcastTran_error_RESTInterface( ) : base()
		{	
		}

		public SdtelectrumRespBroadcastTran_error_RESTInterface( SdtelectrumRespBroadcastTran_error psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("code")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="code", Order=0)]
		public short gxTpr_Code
		{
			get { 
				return sdt.gxTpr_Code;

			}
			set { 
				sdt.gxTpr_Code = value;
			}
		}

		[JsonPropertyName("message")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="message", Order=1)]
		public  string gxTpr_Message
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Message);

			}
			set { 
				 sdt.gxTpr_Message = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtelectrumRespBroadcastTran_error sdt
		{
			get { 
				return (SdtelectrumRespBroadcastTran_error)Sdt;
			}
			set {
				Sdt = value;
			}
		}

		[OnDeserializing]
		void checkSdt( StreamingContext ctx )
		{
			if ( sdt == null )
			{
				sdt = new SdtelectrumRespBroadcastTran_error() ;
			}
		}
	}
	#endregion
}