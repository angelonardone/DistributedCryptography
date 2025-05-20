using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs.wallet {
   public class wwtags : GXDataArea
   {
      public wwtags( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public wwtags( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( )
      {
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         cmbavTagname = new GXCombobox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetNextPar( );
            gxfirstwebparm_bkp = gxfirstwebparm;
            gxfirstwebparm = DecryptAjaxCall( gxfirstwebparm);
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            if ( StringUtil.StrCmp(gxfirstwebparm, "dyncall") == 0 )
            {
               setAjaxCallMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               dyncall( GetNextPar( )) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxEvt") == 0 )
            {
               setAjaxEventMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetNextPar( );
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetNextPar( );
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridonepasswordtags") == 0 )
            {
               gxnrGridonepasswordtags_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridonepasswordtags") == 0 )
            {
               gxgrGridonepasswordtags_refresh_invoke( ) ;
               return  ;
            }
            else
            {
               if ( ! IsValidAjaxCall( false) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = gxfirstwebparm_bkp;
            }
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      protected void gxnrGridonepasswordtags_newrow_invoke( )
      {
         nRC_GXsfl_11 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_11"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_11_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_11_idx = GetPar( "sGXsfl_11_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridonepasswordtags_newrow( ) ;
         /* End function gxnrGridonepasswordtags_newrow_invoke */
      }

      protected void gxgrGridonepasswordtags_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV12Password_tags);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV10onePassword);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridonepasswordtags_refresh( AV12Password_tags, AV10onePassword) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridonepasswordtags_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("general.ui.masterunanimosidebar", "GeneXus.Programs.general.ui.masterunanimosidebar", new Object[] {context});
            MasterPageObj.setDataArea(this,false);
            ValidateSpaRequest();
            MasterPageObj.webExecute();
            if ( ( GxWebError == 0 ) && context.isAjaxRequest( ) )
            {
               enableOutput();
               if ( ! context.isAjaxRequest( ) )
               {
                  context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
               }
               if ( ! context.WillRedirect( ) )
               {
                  AddString( context.getJSONResponse( )) ;
               }
               else
               {
                  if ( context.isAjaxRequest( ) )
                  {
                     disableOutput();
                  }
                  RenderHtmlHeaders( ) ;
                  context.Redirect( context.wjLoc );
                  context.DispatchAjaxCommands();
               }
            }
         }
         cleanup();
      }

      public override short ExecuteStartEvent( )
      {
         PA2K2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START2K2( ) ;
         }
         return gxajaxcallmode ;
      }

      public override void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      public override void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( Form.Caption) ;
         context.WriteHtmlTextNl( "</title>") ;
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( StringUtil.Len( sDynURL) > 0 )
         {
            context.WriteHtmlText( "<BASE href=\""+sDynURL+"\" />") ;
         }
         define_styles( ) ;
         if ( nGXWrapped != 1 )
         {
            MasterPageObj.master_styles();
         }
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 123260), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( Form.Headerrawhtml) ;
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = " data-HasEnter=\"false\" data-Skiponenter=\"false\"";
         context.WriteHtmlText( "<body ") ;
         if ( StringUtil.StrCmp(context.GetLanguageProperty( "rtl"), "true") == 0 )
         {
            context.WriteHtmlText( " dir=\"rtl\" ") ;
         }
         bodyStyle = "" + "background-color:" + context.BuildHTMLColor( Form.Backcolor) + ";color:" + context.BuildHTMLColor( Form.Textcolor) + ";";
         if ( nGXWrapped == 0 )
         {
            bodyStyle += "-moz-opacity:0;opacity:0;";
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( Form.Background)) ) )
         {
            bodyStyle += " background-image:url(" + context.convertURL( Form.Background) + ")";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.wwtags") +"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal Form", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vPASSWORD_TAGS", AV12Password_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vPASSWORD_TAGS", AV12Password_tags);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vPASSWORD_TAGS", GetSecureSignedToken( "", AV12Password_tags, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONEPASSWORD", AV10onePassword);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONEPASSWORD", AV10onePassword);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONEPASSWORD", GetSecureSignedToken( "", AV10onePassword, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Passwordtags", AV15passwordTags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Passwordtags", AV15passwordTags);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_11", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_11), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vPASSWORDTAGS", AV15passwordTags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vPASSWORDTAGS", AV15passwordTags);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vPASSWORD_TAGS", AV12Password_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vPASSWORD_TAGS", AV12Password_tags);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vPASSWORD_TAGS", GetSecureSignedToken( "", AV12Password_tags, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vPASSWORDS", AV13Passwords);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vPASSWORDS", AV13Passwords);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONEPASSWORD", AV10onePassword);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONEPASSWORD", AV10onePassword);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONEPASSWORD", GetSecureSignedToken( "", AV10onePassword, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vPASSWORDS_AND_TAGS", AV14Passwords_and_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vPASSWORDS_AND_TAGS", AV14Passwords_and_tags);
         }
      }

      public override void RenderHtmlCloseForm( )
      {
         SendCloseFormHiddens( ) ;
         GxWebStd.gx_hidden_field( context, "GX_FocusControl", GX_FocusControl);
         SendAjaxEncryptionKey();
         SendSecurityToken((string)(sPrefix));
         SendComponentObjects();
         SendServerCommands();
         SendState();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         context.WriteHtmlTextNl( "</form>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         include_jscripts( ) ;
      }

      public override void RenderHtmlContent( )
      {
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE2K2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT2K2( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return false ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         return formatLink("wallet.wwtags")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.WWTags" ;
      }

      public override string GetPgmdesc( )
      {
         return "Work With Tags" ;
      }

      protected void WB2K0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( nGXWrapped == 1 )
            {
               RenderHtmlHeaders( ) ;
               RenderHtmlOpenForm( ) ;
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavTagname_Internalname+"\"", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'" + sGXsfl_11_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavTagname, cmbavTagname_Internalname, AV16tagName.ToString(), 1, cmbavTagname_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "guid", "", 1, cmbavTagname.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", "", true, 0, "HLP_Wallet/WWTags.htm");
            cmbavTagname.CurrentValue = AV16tagName.ToString();
            AssignProp("", false, cmbavTagname_Internalname, "Values", (string)(cmbavTagname.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridonepasswordtagsContainer.SetWrapped(nGXWrapped);
            StartGridControl11( ) ;
         }
         if ( wbEnd == 11 )
         {
            wbEnd = 0;
            nRC_GXsfl_11 = (int)(nGXsfl_11_idx-1);
            if ( GridonepasswordtagsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV19GXV1 = nGXsfl_11_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridonepasswordtagsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridonepasswordtags", GridonepasswordtagsContainer, subGridonepasswordtags_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridonepasswordtagsContainerData", GridonepasswordtagsContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridonepasswordtagsContainerData"+"V", GridonepasswordtagsContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridonepasswordtagsContainerData"+"V"+"\" value='"+GridonepasswordtagsContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSave_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Save", bttSave_Jsonclick, 5, "Save", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'SAVE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/WWTags.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Cancel", bttCancel_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/WWTags.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 11 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridonepasswordtagsContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV19GXV1 = nGXsfl_11_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridonepasswordtagsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridonepasswordtags", GridonepasswordtagsContainer, subGridonepasswordtags_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridonepasswordtagsContainerData", GridonepasswordtagsContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridonepasswordtagsContainerData"+"V", GridonepasswordtagsContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridonepasswordtagsContainerData"+"V"+"\" value='"+GridonepasswordtagsContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START2K2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_12-186073", 0) ;
            }
         }
         Form.Meta.addItem("description", "Work With Tags", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP2K0( ) ;
      }

      protected void WS2K2( )
      {
         START2K2( ) ;
         EVT2K2( ) ;
      }

      protected void EVT2K2( )
      {
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               sEvt = cgiGet( "_EventName");
               EvtGridId = cgiGet( "_EventGridId");
               EvtRowId = cgiGet( "_EventRowId");
               if ( StringUtil.Len( sEvt) > 0 )
               {
                  sEvtType = StringUtil.Left( sEvt, 1);
                  sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                  if ( StringUtil.StrCmp(sEvtType, "M") != 0 )
                  {
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VTAGNAME.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E112K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Save' */
                              E122K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Cancel' */
                              E132K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 24), "GRIDONEPASSWORDTAGS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'DELETE TAG'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'DELETE TAG'") == 0 ) )
                           {
                              nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
                              SubsflControlProps_112( ) ;
                              AV19GXV1 = nGXsfl_11_idx;
                              if ( ( AV15passwordTags.Count >= AV19GXV1 ) && ( AV19GXV1 > 0 ) )
                              {
                                 AV15passwordTags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV19GXV1));
                                 AV6deleteImage = cgiGet( edtavDeleteimage_Internalname);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV6deleteImage)) ? AV25Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV6deleteImage))), !bGXsfl_11_Refreshing);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV6deleteImage), true);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E142K2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDONEPASSWORDTAGS.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridonepasswordtags.Load */
                                    E152K2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DELETE TAG'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'delete tag' */
                                    E162K2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    if ( ! wbErr )
                                    {
                                       Rfr0gs = false;
                                       if ( ! Rfr0gs )
                                       {
                                       }
                                       dynload_actions( ) ;
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                 }
                              }
                              else
                              {
                              }
                           }
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE2K2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               if ( nGXWrapped == 1 )
               {
                  RenderHtmlCloseForm( ) ;
               }
            }
         }
      }

      protected void PA2K2( )
      {
         if ( nDonePA == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            init_web_controls( ) ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = cmbavTagname_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridonepasswordtags_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_112( ) ;
         while ( nGXsfl_11_idx <= nRC_GXsfl_11 )
         {
            sendrow_112( ) ;
            nGXsfl_11_idx = ((subGridonepasswordtags_Islastpage==1)&&(nGXsfl_11_idx+1>subGridonepasswordtags_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridonepasswordtagsContainer)) ;
         /* End function gxnrGridonepasswordtags_newrow */
      }

      protected void gxgrGridonepasswordtags_refresh( GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV12Password_tags ,
                                                      GeneXus.Programs.wallet.SdtPassword AV10onePassword )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDONEPASSWORDTAGS_nCurrentRecord = 0;
         RF2K2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridonepasswordtags_refresh */
      }

      protected void send_integrity_hashes( )
      {
      }

      protected void clear_multi_value_controls( )
      {
         if ( context.isAjaxRequest( ) )
         {
            dynload_actions( ) ;
            before_start_formulas( ) ;
         }
      }

      protected void fix_multi_value_controls( )
      {
         if ( cmbavTagname.ItemCount > 0 )
         {
            AV16tagName = StringUtil.StrToGuid( cmbavTagname.getValidValue(AV16tagName.ToString()));
            AssignAttri("", false, "AV16tagName", AV16tagName.ToString());
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavTagname.CurrentValue = AV16tagName.ToString();
            AssignProp("", false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF2K2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtltagid_Enabled = 0;
         edtavCtlname_Enabled = 0;
      }

      protected void RF2K2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridonepasswordtagsContainer.ClearRows();
         }
         wbStart = 11;
         nGXsfl_11_idx = 1;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         bGXsfl_11_Refreshing = true;
         GridonepasswordtagsContainer.AddObjectProperty("GridName", "Gridonepasswordtags");
         GridonepasswordtagsContainer.AddObjectProperty("CmpContext", "");
         GridonepasswordtagsContainer.AddObjectProperty("InMasterPage", "false");
         GridonepasswordtagsContainer.AddObjectProperty("Class", "Grid");
         GridonepasswordtagsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridonepasswordtagsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridonepasswordtagsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridonepasswordtags_Backcolorstyle), 1, 0, ".", "")));
         GridonepasswordtagsContainer.PageSize = subGridonepasswordtags_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_112( ) ;
            /* Execute user event: Gridonepasswordtags.Load */
            E152K2 ();
            wbEnd = 11;
            WB2K0( ) ;
         }
         bGXsfl_11_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2K2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vPASSWORD_TAGS", AV12Password_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vPASSWORD_TAGS", AV12Password_tags);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vPASSWORD_TAGS", GetSecureSignedToken( "", AV12Password_tags, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONEPASSWORD", AV10onePassword);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONEPASSWORD", AV10onePassword);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONEPASSWORD", GetSecureSignedToken( "", AV10onePassword, context));
      }

      protected int subGridonepasswordtags_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridonepasswordtags_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridonepasswordtags_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridonepasswordtags_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtltagid_Enabled = 0;
         edtavCtlname_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2K0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E142K2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Passwordtags"), AV15passwordTags);
            ajax_req_read_hidden_sdt(cgiGet( "vPASSWORDTAGS"), AV15passwordTags);
            /* Read saved values. */
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_11_fel_idx = 0;
            while ( nGXsfl_11_fel_idx < nRC_GXsfl_11 )
            {
               nGXsfl_11_fel_idx = ((subGridonepasswordtags_Islastpage==1)&&(nGXsfl_11_fel_idx+1>subGridonepasswordtags_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_fel_idx+1);
               sGXsfl_11_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_112( ) ;
               AV19GXV1 = nGXsfl_11_fel_idx;
               if ( ( AV15passwordTags.Count >= AV19GXV1 ) && ( AV19GXV1 > 0 ) )
               {
                  AV15passwordTags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV19GXV1));
                  AV6deleteImage = cgiGet( edtavDeleteimage_Internalname);
               }
            }
            if ( nGXsfl_11_fel_idx == 0 )
            {
               nGXsfl_11_idx = 1;
               sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
               SubsflControlProps_112( ) ;
            }
            nGXsfl_11_fel_idx = 1;
            /* Read variables values. */
            cmbavTagname.Name = cmbavTagname_Internalname;
            cmbavTagname.CurrentValue = cgiGet( cmbavTagname_Internalname);
            AV16tagName = StringUtil.StrToGuid( cgiGet( cmbavTagname_Internalname));
            AssignAttri("", false, "AV16tagName", AV16tagName.ToString());
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E142K2 ();
         if (returnInSub) return;
      }

      protected void E142K2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV10onePassword.FromJSonString(AV17websession.Get("ONE_PASSWORD_TO_WWTAGS"), null);
         AV14Passwords_and_tags.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "encpasswords.enc", out  AV7error), null);
         AV13Passwords = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV14Passwords_and_tags.gxTpr_Password.Clone());
         AV12Password_tags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV14Passwords_and_tags.gxTpr_Password_tag.Clone());
         cmbavTagname.removeAllItems();
         cmbavTagname.addItem(StringUtil.StrToGuid( "").ToString(), "Select a Tag to add", 0);
         AV22GXV4 = 1;
         while ( AV22GXV4 <= AV12Password_tags.Count )
         {
            AV5oneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV12Password_tags.Item(AV22GXV4));
            cmbavTagname.addItem(AV5oneTag.gxTpr_Tagid.ToString(), AV5oneTag.gxTpr_Name, 0);
            AV22GXV4 = (int)(AV22GXV4+1);
         }
         edtavCtlname_Title = "Tags assigned to "+StringUtil.Trim( AV10onePassword.gxTpr_Description);
         AssignProp("", false, edtavCtlname_Internalname, "Title", edtavCtlname_Title, !bGXsfl_11_Refreshing);
         AV15passwordTags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV10onePassword.gxTpr_Password_tag.Clone());
         gx_BV11 = true;
      }

      protected void E112K2( )
      {
         AV19GXV1 = nGXsfl_11_idx;
         if ( ( AV19GXV1 > 0 ) && ( AV15passwordTags.Count >= AV19GXV1 ) )
         {
            AV15passwordTags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV19GXV1));
         }
         /* Tagname_Controlvaluechanged Routine */
         returnInSub = false;
         AV9found = false;
         AV23GXV5 = 1;
         while ( AV23GXV5 <= AV15passwordTags.Count )
         {
            AV8findOneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV23GXV5));
            if ( AV8findOneTag.gxTpr_Tagid == AV16tagName )
            {
               AV9found = true;
            }
            AV23GXV5 = (int)(AV23GXV5+1);
         }
         if ( ! AV9found && ! (Guid.Empty==AV16tagName) )
         {
            AV11oneTagTemp = new GeneXus.Programs.wallet.SdtPassword_tag(context);
            AV11oneTagTemp.gxTpr_Tagid = AV16tagName;
            AV24GXV6 = 1;
            while ( AV24GXV6 <= AV12Password_tags.Count )
            {
               AV5oneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV12Password_tags.Item(AV24GXV6));
               if ( AV5oneTag.gxTpr_Tagid == AV16tagName )
               {
                  AV11oneTagTemp.gxTpr_Name = AV5oneTag.gxTpr_Name;
               }
               AV24GXV6 = (int)(AV24GXV6+1);
            }
            AV15passwordTags.Add(AV11oneTagTemp, 0);
            gx_BV11 = true;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV15passwordTags", AV15passwordTags);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrGridonepasswordtags_refresh( AV12Password_tags, AV10onePassword) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
      }

      private void E152K2( )
      {
         /* Gridonepasswordtags_Load Routine */
         returnInSub = false;
         AV19GXV1 = 1;
         while ( AV19GXV1 <= AV15passwordTags.Count )
         {
            AV15passwordTags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV19GXV1));
            edtavDeleteimage_gximage = "GeneXusUnanimo_delete_light";
            AV6deleteImage = context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( ));
            AssignAttri("", false, edtavDeleteimage_Internalname, AV6deleteImage);
            AV25Deleteimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( )), context);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 11;
            }
            sendrow_112( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_11_Refreshing )
            {
               DoAjaxLoad(11, GridonepasswordtagsRow);
            }
            AV19GXV1 = (int)(AV19GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E162K2( )
      {
         AV19GXV1 = nGXsfl_11_idx;
         if ( ( AV19GXV1 > 0 ) && ( AV15passwordTags.Count >= AV19GXV1 ) )
         {
            AV15passwordTags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV19GXV1));
         }
         /* 'delete tag' Routine */
         returnInSub = false;
         AV26GXV7 = 1;
         while ( AV26GXV7 <= AV15passwordTags.Count )
         {
            AV8findOneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV26GXV7));
            if ( AV8findOneTag.gxTpr_Tagid == ((GeneXus.Programs.wallet.SdtPassword_tag)(AV15passwordTags.CurrentItem)).gxTpr_Tagid )
            {
               AV15passwordTags.RemoveItem(AV15passwordTags.IndexOf(AV8findOneTag));
               gx_BV11 = true;
            }
            AV26GXV7 = (int)(AV26GXV7+1);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV15passwordTags", AV15passwordTags);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrGridonepasswordtags_refresh( AV12Password_tags, AV10onePassword) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
      }

      protected void E122K2( )
      {
         AV19GXV1 = nGXsfl_11_idx;
         if ( ( AV19GXV1 > 0 ) && ( AV15passwordTags.Count >= AV19GXV1 ) )
         {
            AV15passwordTags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV19GXV1));
         }
         /* 'Save' Routine */
         returnInSub = false;
         AV27GXV8 = 1;
         while ( AV27GXV8 <= AV13Passwords.Count )
         {
            AV18findPassword = ((GeneXus.Programs.wallet.SdtPassword)AV13Passwords.Item(AV27GXV8));
            if ( AV18findPassword.gxTpr_Passwordid == AV10onePassword.gxTpr_Passwordid )
            {
               AV18findPassword.gxTpr_Password_tag.Clear();
               AV28GXV9 = 1;
               while ( AV28GXV9 <= AV15passwordTags.Count )
               {
                  AV5oneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV28GXV9));
                  AV18findPassword.gxTpr_Password_tag.Add(AV5oneTag, 0);
                  AV28GXV9 = (int)(AV28GXV9+1);
               }
            }
            AV27GXV8 = (int)(AV27GXV8+1);
         }
         AV14Passwords_and_tags.gxTpr_Password = AV13Passwords;
         GXt_char1 = AV7error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "encpasswords.enc",  AV14Passwords_and_tags.ToJSonString(false, true), out  GXt_char1) ;
         AV7error = GXt_char1;
         AssignAttri("", false, "AV7error", AV7error);
         AV17websession.Set("ONE_PASSWORD_TO_WWTAGS", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV13Passwords", AV13Passwords);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV14Passwords_and_tags", AV14Passwords_and_tags);
      }

      protected void E132K2( )
      {
         /* 'Cancel' Routine */
         returnInSub = false;
         AV17websession.Set("ONE_PASSWORD_TO_WWTAGS", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
      }

      public override string getresponse( string sGXDynURL )
      {
         initialize_properties( ) ;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         sDynURL = sGXDynURL;
         nGotPars = (short)(1);
         nGXWrapped = (short)(1);
         context.SetWrapped(true);
         PA2K2( ) ;
         WS2K2( ) ;
         WE2K2( ) ;
         cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected void define_styles( )
      {
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202552012594877", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true);
         context.AddJavascriptSource("wallet/wwtags.js", "?202552012594877", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_112( )
      {
         edtavCtltagid_Internalname = "CTLTAGID_"+sGXsfl_11_idx;
         edtavCtlname_Internalname = "CTLNAME_"+sGXsfl_11_idx;
         edtavDeleteimage_Internalname = "vDELETEIMAGE_"+sGXsfl_11_idx;
      }

      protected void SubsflControlProps_fel_112( )
      {
         edtavCtltagid_Internalname = "CTLTAGID_"+sGXsfl_11_fel_idx;
         edtavCtlname_Internalname = "CTLNAME_"+sGXsfl_11_fel_idx;
         edtavDeleteimage_Internalname = "vDELETEIMAGE_"+sGXsfl_11_fel_idx;
      }

      protected void sendrow_112( )
      {
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         WB2K0( ) ;
         GridonepasswordtagsRow = GXWebRow.GetNew(context,GridonepasswordtagsContainer);
         if ( subGridonepasswordtags_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridonepasswordtags_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridonepasswordtags_Class, "") != 0 )
            {
               subGridonepasswordtags_Linesclass = subGridonepasswordtags_Class+"Odd";
            }
         }
         else if ( subGridonepasswordtags_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridonepasswordtags_Backstyle = 0;
            subGridonepasswordtags_Backcolor = subGridonepasswordtags_Allbackcolor;
            if ( StringUtil.StrCmp(subGridonepasswordtags_Class, "") != 0 )
            {
               subGridonepasswordtags_Linesclass = subGridonepasswordtags_Class+"Uniform";
            }
         }
         else if ( subGridonepasswordtags_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridonepasswordtags_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridonepasswordtags_Class, "") != 0 )
            {
               subGridonepasswordtags_Linesclass = subGridonepasswordtags_Class+"Odd";
            }
            subGridonepasswordtags_Backcolor = (int)(0x0);
         }
         else if ( subGridonepasswordtags_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridonepasswordtags_Backstyle = 1;
            if ( ((int)((nGXsfl_11_idx) % (2))) == 0 )
            {
               subGridonepasswordtags_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridonepasswordtags_Class, "") != 0 )
               {
                  subGridonepasswordtags_Linesclass = subGridonepasswordtags_Class+"Even";
               }
            }
            else
            {
               subGridonepasswordtags_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridonepasswordtags_Class, "") != 0 )
               {
                  subGridonepasswordtags_Linesclass = subGridonepasswordtags_Class+"Odd";
               }
            }
         }
         if ( GridonepasswordtagsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_11_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridonepasswordtagsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         GridonepasswordtagsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtltagid_Internalname,((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV19GXV1)).gxTpr_Tagid.ToString(),((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV19GXV1)).gxTpr_Tagid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtltagid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtltagid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)11,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridonepasswordtagsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridonepasswordtagsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlname_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtPassword_tag)AV15passwordTags.Item(AV19GXV1)).gxTpr_Name),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlname_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlname_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)80,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridonepasswordtagsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'',false,'',11)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class");
         StyleString = "";
         AV6deleteImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV6deleteImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV25Deleteimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV6deleteImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV6deleteImage)) ? AV25Deleteimage_GXI : context.PathToRelativeUrl( AV6deleteImage));
         GridonepasswordtagsRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"delete",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavDeleteimage_Jsonclick,"'"+""+"'"+",false,"+"'"+"E\\'DELETE TAG\\'."+sGXsfl_11_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV6deleteImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         send_integrity_lvl_hashes2K2( ) ;
         GridonepasswordtagsContainer.AddRow(GridonepasswordtagsRow);
         nGXsfl_11_idx = ((subGridonepasswordtags_Islastpage==1)&&(nGXsfl_11_idx+1>subGridonepasswordtags_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         /* End function sendrow_112 */
      }

      protected void init_web_controls( )
      {
         cmbavTagname.Name = "vTAGNAME";
         cmbavTagname.WebTags = "";
         if ( cmbavTagname.ItemCount > 0 )
         {
            AV16tagName = StringUtil.StrToGuid( cmbavTagname.getValidValue(AV16tagName.ToString()));
            AssignAttri("", false, "AV16tagName", AV16tagName.ToString());
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl11( )
      {
         if ( GridonepasswordtagsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridonepasswordtagsContainer"+"DivS\" data-gxgridid=\"11\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridonepasswordtags_Internalname, subGridonepasswordtags_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridonepasswordtags_Backcolorstyle == 0 )
            {
               subGridonepasswordtags_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridonepasswordtags_Class) > 0 )
               {
                  subGridonepasswordtags_Linesclass = subGridonepasswordtags_Class+"Title";
               }
            }
            else
            {
               subGridonepasswordtags_Titlebackstyle = 1;
               if ( subGridonepasswordtags_Backcolorstyle == 1 )
               {
                  subGridonepasswordtags_Titlebackcolor = subGridonepasswordtags_Allbackcolor;
                  if ( StringUtil.Len( subGridonepasswordtags_Class) > 0 )
                  {
                     subGridonepasswordtags_Linesclass = subGridonepasswordtags_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridonepasswordtags_Class) > 0 )
                  {
                     subGridonepasswordtags_Linesclass = subGridonepasswordtags_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "Tag Id") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( edtavCtlname_Title) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class")+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridonepasswordtagsContainer.AddObjectProperty("GridName", "Gridonepasswordtags");
         }
         else
         {
            GridonepasswordtagsContainer.AddObjectProperty("GridName", "Gridonepasswordtags");
            GridonepasswordtagsContainer.AddObjectProperty("Header", subGridonepasswordtags_Header);
            GridonepasswordtagsContainer.AddObjectProperty("Class", "Grid");
            GridonepasswordtagsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridonepasswordtagsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridonepasswordtagsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridonepasswordtags_Backcolorstyle), 1, 0, ".", "")));
            GridonepasswordtagsContainer.AddObjectProperty("CmpContext", "");
            GridonepasswordtagsContainer.AddObjectProperty("InMasterPage", "false");
            GridonepasswordtagsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridonepasswordtagsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtltagid_Enabled), 5, 0, ".", "")));
            GridonepasswordtagsContainer.AddColumnProperties(GridonepasswordtagsColumn);
            GridonepasswordtagsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridonepasswordtagsColumn.AddObjectProperty("Title", StringUtil.RTrim( edtavCtlname_Title));
            GridonepasswordtagsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlname_Enabled), 5, 0, ".", "")));
            GridonepasswordtagsContainer.AddColumnProperties(GridonepasswordtagsColumn);
            GridonepasswordtagsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridonepasswordtagsColumn.AddObjectProperty("Value", context.convertURL( AV6deleteImage));
            GridonepasswordtagsContainer.AddColumnProperties(GridonepasswordtagsColumn);
            GridonepasswordtagsContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridonepasswordtags_Selectedindex), 4, 0, ".", "")));
            GridonepasswordtagsContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridonepasswordtags_Allowselection), 1, 0, ".", "")));
            GridonepasswordtagsContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridonepasswordtags_Selectioncolor), 9, 0, ".", "")));
            GridonepasswordtagsContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridonepasswordtags_Allowhovering), 1, 0, ".", "")));
            GridonepasswordtagsContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridonepasswordtags_Hoveringcolor), 9, 0, ".", "")));
            GridonepasswordtagsContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridonepasswordtags_Allowcollapsing), 1, 0, ".", "")));
            GridonepasswordtagsContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridonepasswordtags_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         cmbavTagname_Internalname = "vTAGNAME";
         edtavCtltagid_Internalname = "CTLTAGID";
         edtavCtlname_Internalname = "CTLNAME";
         edtavDeleteimage_Internalname = "vDELETEIMAGE";
         bttSave_Internalname = "SAVE";
         bttCancel_Internalname = "CANCEL";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subGridonepasswordtags_Internalname = "GRIDONEPASSWORDTAGS";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridonepasswordtags_Allowcollapsing = 0;
         subGridonepasswordtags_Allowselection = 0;
         subGridonepasswordtags_Header = "";
         edtavCtlname_Title = "Name";
         edtavDeleteimage_Jsonclick = "";
         edtavDeleteimage_gximage = "";
         edtavCtlname_Jsonclick = "";
         edtavCtlname_Enabled = 0;
         edtavCtltagid_Jsonclick = "";
         edtavCtltagid_Enabled = 0;
         subGridonepasswordtags_Class = "Grid";
         subGridonepasswordtags_Backcolorstyle = 0;
         edtavCtlname_Title = "Name";
         edtavCtlname_Enabled = -1;
         edtavCtltagid_Enabled = -1;
         cmbavTagname_Jsonclick = "";
         cmbavTagname.Enabled = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Work With Tags";
         if ( context.isSpaRequest( ) )
         {
            enableJsOutput();
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDONEPASSWORDTAGS_nFirstRecordOnPage","type":"int"},{"av":"GRIDONEPASSWORDTAGS_nEOF","type":"int"},{"av":"AV15passwordTags","fld":"vPASSWORDTAGS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDONEPASSWORDTAGS","prop":"GridRC","grid":11,"type":"int"},{"av":"AV12Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV10onePassword","fld":"vONEPASSWORD","hsh":true,"type":""}]}""");
         setEventMetadata("VTAGNAME.CONTROLVALUECHANGED","""{"handler":"E112K2","iparms":[{"av":"AV15passwordTags","fld":"vPASSWORDTAGS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDONEPASSWORDTAGS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDONEPASSWORDTAGS","prop":"GridRC","grid":11,"type":"int"},{"av":"cmbavTagname"},{"av":"AV16tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV12Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"GRIDONEPASSWORDTAGS_nEOF","type":"int"},{"av":"AV10onePassword","fld":"vONEPASSWORD","hsh":true,"type":""}]""");
         setEventMetadata("VTAGNAME.CONTROLVALUECHANGED",""","oparms":[{"av":"AV15passwordTags","fld":"vPASSWORDTAGS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDONEPASSWORDTAGS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDONEPASSWORDTAGS","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("GRIDONEPASSWORDTAGS.LOAD","""{"handler":"E152K2","iparms":[]""");
         setEventMetadata("GRIDONEPASSWORDTAGS.LOAD",""","oparms":[{"av":"AV6deleteImage","fld":"vDELETEIMAGE","type":"bits"}]}""");
         setEventMetadata("'DELETE TAG'","""{"handler":"E162K2","iparms":[{"av":"AV15passwordTags","fld":"vPASSWORDTAGS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDONEPASSWORDTAGS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDONEPASSWORDTAGS","prop":"GridRC","grid":11,"type":"int"},{"av":"GRIDONEPASSWORDTAGS_nEOF","type":"int"},{"av":"AV12Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV10onePassword","fld":"vONEPASSWORD","hsh":true,"type":""}]""");
         setEventMetadata("'DELETE TAG'",""","oparms":[{"av":"AV15passwordTags","fld":"vPASSWORDTAGS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDONEPASSWORDTAGS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDONEPASSWORDTAGS","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'SAVE'","""{"handler":"E122K2","iparms":[{"av":"AV13Passwords","fld":"vPASSWORDS","type":""},{"av":"AV10onePassword","fld":"vONEPASSWORD","hsh":true,"type":""},{"av":"AV15passwordTags","fld":"vPASSWORDTAGS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDONEPASSWORDTAGS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDONEPASSWORDTAGS","prop":"GridRC","grid":11,"type":"int"},{"av":"AV14Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""}]""");
         setEventMetadata("'SAVE'",""","oparms":[{"av":"AV13Passwords","fld":"vPASSWORDS","type":""},{"av":"AV14Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV7error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E132K2","iparms":[]}""");
         setEventMetadata("VALIDV_TAGNAME","""{"handler":"Validv_Tagname","iparms":[]}""");
         setEventMetadata("VALIDV_GXV2","""{"handler":"Validv_Gxv2","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Deleteimage","iparms":[]}""");
         return  ;
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      public override void initialize( )
      {
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV12Password_tags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography");
         AV10onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV15passwordTags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography");
         AV13Passwords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "distributedcryptography");
         AV14Passwords_and_tags = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         AV16tagName = Guid.Empty;
         GridonepasswordtagsContainer = new GXWebGrid( context);
         sStyleString = "";
         ClassString = "";
         StyleString = "";
         bttSave_Jsonclick = "";
         bttCancel_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV6deleteImage = "";
         AV25Deleteimage_GXI = "";
         AV17websession = context.GetSession();
         AV7error = "";
         AV5oneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV8findOneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV11oneTagTemp = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         GridonepasswordtagsRow = new GXWebRow();
         AV18findPassword = new GeneXus.Programs.wallet.SdtPassword(context);
         GXt_char1 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridonepasswordtags_Linesclass = "";
         ROClassString = "";
         sImgUrl = "";
         GridonepasswordtagsColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtltagid_Enabled = 0;
         edtavCtlname_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridonepasswordtags_Backcolorstyle ;
      private short GRIDONEPASSWORDTAGS_nEOF ;
      private short nGXWrapped ;
      private short subGridonepasswordtags_Backstyle ;
      private short subGridonepasswordtags_Titlebackstyle ;
      private short subGridonepasswordtags_Allowselection ;
      private short subGridonepasswordtags_Allowhovering ;
      private short subGridonepasswordtags_Allowcollapsing ;
      private short subGridonepasswordtags_Collapsed ;
      private int nRC_GXsfl_11 ;
      private int nGXsfl_11_idx=1 ;
      private int AV19GXV1 ;
      private int subGridonepasswordtags_Islastpage ;
      private int edtavCtltagid_Enabled ;
      private int edtavCtlname_Enabled ;
      private int nGXsfl_11_fel_idx=1 ;
      private int AV22GXV4 ;
      private int AV23GXV5 ;
      private int AV24GXV6 ;
      private int nGXsfl_11_bak_idx=1 ;
      private int AV26GXV7 ;
      private int AV27GXV8 ;
      private int AV28GXV9 ;
      private int idxLst ;
      private int subGridonepasswordtags_Backcolor ;
      private int subGridonepasswordtags_Allbackcolor ;
      private int subGridonepasswordtags_Titlebackcolor ;
      private int subGridonepasswordtags_Selectedindex ;
      private int subGridonepasswordtags_Selectioncolor ;
      private int subGridonepasswordtags_Hoveringcolor ;
      private long GRIDONEPASSWORDTAGS_nCurrentRecord ;
      private long GRIDONEPASSWORDTAGS_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_11_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string cmbavTagname_Internalname ;
      private string TempTags ;
      private string cmbavTagname_Jsonclick ;
      private string sStyleString ;
      private string subGridonepasswordtags_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string bttSave_Internalname ;
      private string bttSave_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavDeleteimage_Internalname ;
      private string sGXsfl_11_fel_idx="0001" ;
      private string AV7error ;
      private string edtavCtlname_Title ;
      private string edtavCtlname_Internalname ;
      private string edtavDeleteimage_gximage ;
      private string GXt_char1 ;
      private string edtavCtltagid_Internalname ;
      private string subGridonepasswordtags_Class ;
      private string subGridonepasswordtags_Linesclass ;
      private string ROClassString ;
      private string edtavCtltagid_Jsonclick ;
      private string edtavCtlname_Jsonclick ;
      private string sImgUrl ;
      private string edtavDeleteimage_Jsonclick ;
      private string subGridonepasswordtags_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_11_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV11 ;
      private bool AV9found ;
      private bool AV6deleteImage_IsBlob ;
      private string AV25Deleteimage_GXI ;
      private string AV6deleteImage ;
      private Guid AV16tagName ;
      private IGxSession AV17websession ;
      private GXWebGrid GridonepasswordtagsContainer ;
      private GXWebRow GridonepasswordtagsRow ;
      private GXWebColumn GridonepasswordtagsColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavTagname ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV12Password_tags ;
      private GeneXus.Programs.wallet.SdtPassword AV10onePassword ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV15passwordTags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV13Passwords ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV14Passwords_and_tags ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV5oneTag ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV8findOneTag ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV11oneTagTemp ;
      private GeneXus.Programs.wallet.SdtPassword AV18findPassword ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
