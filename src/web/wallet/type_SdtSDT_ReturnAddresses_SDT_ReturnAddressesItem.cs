/*
				   File: type_SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem
			Description: SDT_ReturnAddresses
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
	[XmlRoot(ElementName="SDT_ReturnAddressesItem")]
	[XmlType(TypeName="SDT_ReturnAddressesItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem : GxUserType
	{
		public SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_Address = "";

		}

		public SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem(IGxContext context)
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
			AddObjectProperty("Address", gxTpr_Address, false);


			AddObjectProperty("isUsed", gxTpr_Isused, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Address")]
		[XmlElement(ElementName="Address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_Address; 
			}
			set {
				gxTv_SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="isUsed")]
		[XmlElement(ElementName="isUsed")]
		public bool gxTpr_Isused
		{
			get {
				return gxTv_SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_Isused; 
			}
			set {
				gxTv_SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_Isused = value;
				SetDirty("Isused");
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
			gxTv_SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_Address = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_Address;
		 

		protected bool gxTv_SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_Isused;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"SDT_ReturnAddressesItem", Namespace="distributedcryptography")]
	public class SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_RESTInterface : GxGenericCollectionItem<SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem_RESTInterface( SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Address", Order=0)]
		public  string gxTpr_Address
		{
			get { 
				return sdt.gxTpr_Address;

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[DataMember(Name="isUsed", Order=1)]
		public bool gxTpr_Isused
		{
			get { 
				return sdt.gxTpr_Isused;

			}
			set { 
				sdt.gxTpr_Isused = value;
			}
		}


		#endregion

		public SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem sdt
		{
			get { 
				return (SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem)Sdt;
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
				sdt = new SdtSDT_ReturnAddresses_SDT_ReturnAddressesItem() ;
			}
		}
	}
	#endregion
}