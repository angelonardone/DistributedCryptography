/*
				   File: type_SdtGetTransactions__postInput
			Description: GetTransactions__postInput
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="GetTransactions__postInput")]
	[XmlType(TypeName="GetTransactions__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGetTransactions__postInput : GxUserType
	{
		public SdtGetTransactions__postInput( )
		{
			/* Constructor for serialization */
		}

		public SdtGetTransactions__postInput(IGxContext context)
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
			if (gxTv_SdtGetTransactions__postInput_Sdt_addressess != null)
			{
				AddObjectProperty("SDT_Addressess", gxTv_SdtGetTransactions__postInput_Sdt_addressess, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="SDT_Addressess")]
		[XmlElement(ElementName="SDT_Addressess")]
		public GeneXus.Programs.SdtNBitcoin_SDT_Addressess gxTpr_Sdt_addressess
		{
			get {
				if ( gxTv_SdtGetTransactions__postInput_Sdt_addressess == null )
				{
					gxTv_SdtGetTransactions__postInput_Sdt_addressess = new GeneXus.Programs.SdtNBitcoin_SDT_Addressess(context);
				}
				return gxTv_SdtGetTransactions__postInput_Sdt_addressess; 
			}
			set {
				gxTv_SdtGetTransactions__postInput_Sdt_addressess = value;
				SetDirty("Sdt_addressess");
			}
		}
		public void gxTv_SdtGetTransactions__postInput_Sdt_addressess_SetNull()
		{
			gxTv_SdtGetTransactions__postInput_Sdt_addressess_N = true;
			gxTv_SdtGetTransactions__postInput_Sdt_addressess = null;
		}

		public bool gxTv_SdtGetTransactions__postInput_Sdt_addressess_IsNull()
		{
			return gxTv_SdtGetTransactions__postInput_Sdt_addressess == null;
		}
		public bool ShouldSerializegxTpr_Sdt_addressess_Json()
		{
			return gxTv_SdtGetTransactions__postInput_Sdt_addressess != null;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return (
				 ShouldSerializegxTpr_Sdt_addressess_Json()||  
				false);
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtGetTransactions__postInput_Sdt_addressess_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected GeneXus.Programs.SdtNBitcoin_SDT_Addressess gxTv_SdtGetTransactions__postInput_Sdt_addressess = null;
		protected bool gxTv_SdtGetTransactions__postInput_Sdt_addressess_N;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"GetTransactions__postInput", Namespace="distributedcryptography")]
	public class SdtGetTransactions__postInput_RESTInterface : GxGenericCollectionItem<SdtGetTransactions__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGetTransactions__postInput_RESTInterface( ) : base()
		{	
		}

		public SdtGetTransactions__postInput_RESTInterface( SdtGetTransactions__postInput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="SDT_Addressess", Order=0, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtNBitcoin_SDT_Addressess_RESTInterface gxTpr_Sdt_addressess
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Sdt_addressess_Json())
					return new GeneXus.Programs.SdtNBitcoin_SDT_Addressess_RESTInterface(sdt.gxTpr_Sdt_addressess);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Sdt_addressess = value.sdt;
			}
		}


		#endregion

		public SdtGetTransactions__postInput sdt
		{
			get { 
				return (SdtGetTransactions__postInput)Sdt;
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
				sdt = new SdtGetTransactions__postInput() ;
			}
		}
	}
	#endregion
}