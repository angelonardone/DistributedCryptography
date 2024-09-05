/*
				   File: type_SdtSdtChosenResultItem
			Description: SdtChosenResultItem
				 Author: Nemo 🐠 for C# (.NET) version 18.0.10.184260
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

using GeneXus.Programs;
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="SdtChosenResultItem")]
	[XmlType(TypeName="SdtChosenResultItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSdtChosenResultItem : GxUserType
	{
		public SdtSdtChosenResultItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtSdtChosenResultItem_Value = "";

		}

		public SdtSdtChosenResultItem(IGxContext context)
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
			AddObjectProperty("value", gxTpr_Value, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="value")]
		[XmlElement(ElementName="value")]
		public string gxTpr_Value
		{
			get {
				return gxTv_SdtSdtChosenResultItem_Value; 
			}
			set {
				gxTv_SdtSdtChosenResultItem_Value = value;
				SetDirty("Value");
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
			gxTv_SdtSdtChosenResultItem_Value = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSdtChosenResultItem_Value;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"SdtChosenResultItem", Namespace="distributedcryptography")]
	public class SdtSdtChosenResultItem_RESTInterface : GxGenericCollectionItem<SdtSdtChosenResultItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSdtChosenResultItem_RESTInterface( ) : base()
		{	
		}

		public SdtSdtChosenResultItem_RESTInterface( SdtSdtChosenResultItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="value", Order=0)]
		public  string gxTpr_Value
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Value);

			}
			set { 
				 sdt.gxTpr_Value = value;
			}
		}


		#endregion

		public SdtSdtChosenResultItem sdt
		{
			get { 
				return (SdtSdtChosenResultItem)Sdt;
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
				sdt = new SdtSdtChosenResultItem() ;
			}
		}
	}
	#endregion
}