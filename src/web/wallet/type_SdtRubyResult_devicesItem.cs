/*
				   File: type_SdtRubyResult_devicesItem
			Description: devices
				 Author: Nemo 🐠 for C# (.NET) version 18.0.16.189595
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

namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="RubyResult.devicesItem")]
	[XmlType(TypeName="RubyResult.devicesItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtRubyResult_devicesItem : GxUserType
	{
		public SdtRubyResult_devicesItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtRubyResult_devicesItem_Device = "";

			gxTv_SdtRubyResult_devicesItem_Version = "";

			gxTv_SdtRubyResult_devicesItem_State = "";

		}

		public SdtRubyResult_devicesItem(IGxContext context)
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
			AddObjectProperty("device", gxTpr_Device, false);


			AddObjectProperty("version", gxTpr_Version, false);


			AddObjectProperty("state", gxTpr_State, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="device")]
		[XmlElement(ElementName="device")]
		public string gxTpr_Device
		{
			get {
				return gxTv_SdtRubyResult_devicesItem_Device; 
			}
			set {
				gxTv_SdtRubyResult_devicesItem_Device = value;
				SetDirty("Device");
			}
		}




		[SoapElement(ElementName="version")]
		[XmlElement(ElementName="version")]
		public string gxTpr_Version
		{
			get {
				return gxTv_SdtRubyResult_devicesItem_Version; 
			}
			set {
				gxTv_SdtRubyResult_devicesItem_Version = value;
				SetDirty("Version");
			}
		}




		[SoapElement(ElementName="state")]
		[XmlElement(ElementName="state")]
		public string gxTpr_State
		{
			get {
				return gxTv_SdtRubyResult_devicesItem_State; 
			}
			set {
				gxTv_SdtRubyResult_devicesItem_State = value;
				SetDirty("State");
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
			gxTv_SdtRubyResult_devicesItem_Device = "";
			gxTv_SdtRubyResult_devicesItem_Version = "";
			gxTv_SdtRubyResult_devicesItem_State = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtRubyResult_devicesItem_Device;
		 

		protected string gxTv_SdtRubyResult_devicesItem_Version;
		 

		protected string gxTv_SdtRubyResult_devicesItem_State;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"RubyResult.devicesItem", Namespace="distributedcryptography")]
	public class SdtRubyResult_devicesItem_RESTInterface : GxGenericCollectionItem<SdtRubyResult_devicesItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtRubyResult_devicesItem_RESTInterface( ) : base()
		{	
		}

		public SdtRubyResult_devicesItem_RESTInterface( SdtRubyResult_devicesItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("device")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="device", Order=0)]
		public  string gxTpr_Device
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Device);

			}
			set { 
				 sdt.gxTpr_Device = value;
			}
		}

		[JsonPropertyName("version")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="version", Order=1)]
		public  string gxTpr_Version
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Version);

			}
			set { 
				 sdt.gxTpr_Version = value;
			}
		}

		[JsonPropertyName("state")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="state", Order=2)]
		public  string gxTpr_State
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_State);

			}
			set { 
				 sdt.gxTpr_State = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtRubyResult_devicesItem sdt
		{
			get { 
				return (SdtRubyResult_devicesItem)Sdt;
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
				sdt = new SdtRubyResult_devicesItem() ;
			}
		}
	}
	#endregion
}