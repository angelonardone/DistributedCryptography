/*
				   File: type_SdtSdtChosen_SdtChosenItem
			Description: SdtChosen
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
	[XmlRoot(ElementName="SdtChosenItem")]
	[XmlType(TypeName="SdtChosenItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSdtChosen_SdtChosenItem : GxUserType
	{
		public SdtSdtChosen_SdtChosenItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtSdtChosen_SdtChosenItem_Id = "";

			gxTv_SdtSdtChosen_SdtChosenItem_Value = "";

		}

		public SdtSdtChosen_SdtChosenItem(IGxContext context)
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
			AddObjectProperty("Id", gxTpr_Id, false);


			AddObjectProperty("Value", gxTpr_Value, false);


			AddObjectProperty("Selected", gxTpr_Selected, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Id")]
		[XmlElement(ElementName="Id")]
		public string gxTpr_Id
		{
			get {
				return gxTv_SdtSdtChosen_SdtChosenItem_Id; 
			}
			set {
				gxTv_SdtSdtChosen_SdtChosenItem_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="Value")]
		[XmlElement(ElementName="Value")]
		public string gxTpr_Value
		{
			get {
				return gxTv_SdtSdtChosen_SdtChosenItem_Value; 
			}
			set {
				gxTv_SdtSdtChosen_SdtChosenItem_Value = value;
				SetDirty("Value");
			}
		}




		[SoapElement(ElementName="Selected")]
		[XmlElement(ElementName="Selected")]
		public bool gxTpr_Selected
		{
			get {
				return gxTv_SdtSdtChosen_SdtChosenItem_Selected; 
			}
			set {
				gxTv_SdtSdtChosen_SdtChosenItem_Selected = value;
				SetDirty("Selected");
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
			gxTv_SdtSdtChosen_SdtChosenItem_Id = "";
			gxTv_SdtSdtChosen_SdtChosenItem_Value = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSdtChosen_SdtChosenItem_Id;
		 

		protected string gxTv_SdtSdtChosen_SdtChosenItem_Value;
		 

		protected bool gxTv_SdtSdtChosen_SdtChosenItem_Selected;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"SdtChosenItem", Namespace="distributedcryptography")]
	public class SdtSdtChosen_SdtChosenItem_RESTInterface : GxGenericCollectionItem<SdtSdtChosen_SdtChosenItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSdtChosen_SdtChosenItem_RESTInterface( ) : base()
		{	
		}

		public SdtSdtChosen_SdtChosenItem_RESTInterface( SdtSdtChosen_SdtChosenItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Id", Order=0)]
		public  string gxTpr_Id
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Id);

			}
			set { 
				 sdt.gxTpr_Id = value;
			}
		}

		[DataMember(Name="Value", Order=1)]
		public  string gxTpr_Value
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Value);

			}
			set { 
				 sdt.gxTpr_Value = value;
			}
		}

		[DataMember(Name="Selected", Order=2)]
		public bool gxTpr_Selected
		{
			get { 
				return sdt.gxTpr_Selected;

			}
			set { 
				sdt.gxTpr_Selected = value;
			}
		}


		#endregion

		public SdtSdtChosen_SdtChosenItem sdt
		{
			get { 
				return (SdtSdtChosen_SdtChosenItem)Sdt;
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
				sdt = new SdtSdtChosen_SdtChosenItem() ;
			}
		}
	}
	#endregion
}