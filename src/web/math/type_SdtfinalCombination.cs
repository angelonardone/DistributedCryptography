/*
				   File: type_SdtfinalCombination
			Description: finalCombination
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
namespace GeneXus.Programs.math
{
	[XmlRoot(ElementName="finalCombination")]
	[XmlType(TypeName="finalCombination" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtfinalCombination : GxUserType
	{
		public SdtfinalCombination( )
		{
			/* Constructor for serialization */
		}

		public SdtfinalCombination(IGxContext context)
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
			if (gxTv_SdtfinalCombination_Items != null)
			{
				AddObjectProperty("Items", gxTv_SdtfinalCombination_Items, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Items" )]
		[XmlArray(ElementName="Items"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GxSimpleCollection<string> gxTpr_Items_GxSimpleCollection
		{
			get {
				if ( gxTv_SdtfinalCombination_Items == null )
				{
					gxTv_SdtfinalCombination_Items = new GxSimpleCollection<string>( );
				}
				return gxTv_SdtfinalCombination_Items;
			}
			set {
				gxTv_SdtfinalCombination_Items_N = false;
				gxTv_SdtfinalCombination_Items = value;
			}
		}

		[XmlIgnore]
		public GxSimpleCollection<string> gxTpr_Items
		{
			get {
				if ( gxTv_SdtfinalCombination_Items == null )
				{
					gxTv_SdtfinalCombination_Items = new GxSimpleCollection<string>();
				}
				gxTv_SdtfinalCombination_Items_N = false;
				return gxTv_SdtfinalCombination_Items ;
			}
			set {
				gxTv_SdtfinalCombination_Items_N = false;
				gxTv_SdtfinalCombination_Items = value;
				SetDirty("Items");
			}
		}

		public void gxTv_SdtfinalCombination_Items_SetNull()
		{
			gxTv_SdtfinalCombination_Items_N = true;
			gxTv_SdtfinalCombination_Items = null;
		}

		public bool gxTv_SdtfinalCombination_Items_IsNull()
		{
			return gxTv_SdtfinalCombination_Items == null;
		}
		public bool ShouldSerializegxTpr_Items_GxSimpleCollection_Json()
		{
			return gxTv_SdtfinalCombination_Items != null && gxTv_SdtfinalCombination_Items.Count > 0;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return (
				 ShouldSerializegxTpr_Items_GxSimpleCollection_Json()||  
				false);
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
			gxTv_SdtfinalCombination_Items_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtfinalCombination_Items_N;
		protected GxSimpleCollection<string> gxTv_SdtfinalCombination_Items = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"finalCombination", Namespace="distributedcryptography")]
	public class SdtfinalCombination_RESTInterface : GxGenericCollectionItem<SdtfinalCombination>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtfinalCombination_RESTInterface( ) : base()
		{	
		}

		public SdtfinalCombination_RESTInterface( SdtfinalCombination psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Items", Order=0, EmitDefaultValue=false)]
		public  GxSimpleCollection<string> gxTpr_Items
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Items_GxSimpleCollection_Json())
					return sdt.gxTpr_Items;
				else
					return null;

			}
			set { 
				sdt.gxTpr_Items = value ;
			}
		}


		#endregion

		public SdtfinalCombination sdt
		{
			get { 
				return (SdtfinalCombination)Sdt;
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
				sdt = new SdtfinalCombination() ;
			}
		}
	}
	#endregion
}