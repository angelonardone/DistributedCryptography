/*
				   File: type_SdtSDT_Addressess_SDT_AddressessItem
			Description: SDT_Addressess
				 Author: Nemo 🐠 for C# (.NET) version 18.0.8.180599
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
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="SDT_AddressessItem")]
	[XmlType(TypeName="SDT_AddressessItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSDT_Addressess_SDT_AddressessItem : GxUserType
	{
		public SdtSDT_Addressess_SDT_AddressessItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_Addressess_SDT_AddressessItem_Address = "";

		}

		public SdtSDT_Addressess_SDT_AddressessItem(IGxContext context)
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


			AddObjectProperty("GeneratedType", gxTpr_Generatedtype, false);


			AddObjectProperty("CreationSequence", gxTpr_Creationsequence, false);


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
				return gxTv_SdtSDT_Addressess_SDT_AddressessItem_Address; 
			}
			set {
				gxTv_SdtSDT_Addressess_SDT_AddressessItem_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="GeneratedType")]
		[XmlElement(ElementName="GeneratedType")]
		public short gxTpr_Generatedtype
		{
			get {
				return gxTv_SdtSDT_Addressess_SDT_AddressessItem_Generatedtype; 
			}
			set {
				gxTv_SdtSDT_Addressess_SDT_AddressessItem_Generatedtype = value;
				SetDirty("Generatedtype");
			}
		}




		[SoapElement(ElementName="CreationSequence")]
		[XmlElement(ElementName="CreationSequence")]
		public long gxTpr_Creationsequence
		{
			get {
				return gxTv_SdtSDT_Addressess_SDT_AddressessItem_Creationsequence; 
			}
			set {
				gxTv_SdtSDT_Addressess_SDT_AddressessItem_Creationsequence = value;
				SetDirty("Creationsequence");
			}
		}




		[SoapElement(ElementName="isUsed")]
		[XmlElement(ElementName="isUsed")]
		public bool gxTpr_Isused
		{
			get {
				return gxTv_SdtSDT_Addressess_SDT_AddressessItem_Isused; 
			}
			set {
				gxTv_SdtSDT_Addressess_SDT_AddressessItem_Isused = value;
				SetDirty("Isused");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtSDT_Addressess_SDT_AddressessItem_Address = "";



			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSDT_Addressess_SDT_AddressessItem_Address;
		 

		protected short gxTv_SdtSDT_Addressess_SDT_AddressessItem_Generatedtype;
		 

		protected long gxTv_SdtSDT_Addressess_SDT_AddressessItem_Creationsequence;
		 

		protected bool gxTv_SdtSDT_Addressess_SDT_AddressessItem_Isused;
		 


		#endregion
	}
	#region Rest interface
	[DataContract(Name=@"SDT_AddressessItem", Namespace="distributedcryptography")]
	public class SdtSDT_Addressess_SDT_AddressessItem_RESTInterface : GxGenericCollectionItem<SdtSDT_Addressess_SDT_AddressessItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_Addressess_SDT_AddressessItem_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_Addressess_SDT_AddressessItem_RESTInterface( SdtSDT_Addressess_SDT_AddressessItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Address", Order=0)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[DataMember(Name="GeneratedType", Order=1)]
		public short gxTpr_Generatedtype
		{
			get { 
				return sdt.gxTpr_Generatedtype;

			}
			set { 
				sdt.gxTpr_Generatedtype = value;
			}
		}

		[DataMember(Name="CreationSequence", Order=2)]
		public  string gxTpr_Creationsequence
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Creationsequence, 10, 0));

			}
			set { 
				sdt.gxTpr_Creationsequence = (long) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="isUsed", Order=3)]
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

		public SdtSDT_Addressess_SDT_AddressessItem sdt
		{
			get { 
				return (SdtSDT_Addressess_SDT_AddressessItem)Sdt;
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
				sdt = new SdtSDT_Addressess_SDT_AddressessItem() ;
			}
		}
	}
	#endregion
}