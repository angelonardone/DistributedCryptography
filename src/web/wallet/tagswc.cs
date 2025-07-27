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
using System.Text.Json.Serialization;
namespace GeneXus.Programs.wallet {
   public class tagswc : GXWebComponent
   {
      public tagswc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
      }

      public tagswc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref Guid aP0_groupId )
      {
         this.AV18groupId = aP0_groupId;
         ExecuteImpl();
         aP0_groupId=this.AV18groupId;
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      public override void SetPrefix( string sPPrefix )
      {
         sPrefix = sPPrefix;
      }

      protected override void createObjects( )
      {
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            if ( nGotPars == 0 )
            {
               entryPointCalled = false;
               gxfirstwebparm = GetFirstPar( "groupId");
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "dyncomponent") == 0 )
               {
                  setAjaxEventMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  nDynComponent = 1;
                  sCompPrefix = GetPar( "sCompPrefix");
                  sSFPrefix = GetPar( "sSFPrefix");
                  AV18groupId = StringUtil.StrToGuid( GetPar( "groupId"));
                  AssignAttri(sPrefix, false, "AV18groupId", AV18groupId.ToString());
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(Guid)AV18groupId});
                  componentstart();
                  context.httpAjaxContext.ajax_rspStartCmp(sPrefix);
                  componentdraw();
                  context.httpAjaxContext.ajax_rspEndCmp();
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
                  gxfirstwebparm = GetFirstPar( "groupId");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetFirstPar( "groupId");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Taggrid") == 0 )
               {
                  gxnrTaggrid_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Taggrid") == 0 )
               {
                  gxgrTaggrid_refresh_invoke( ) ;
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
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      protected void gxnrTaggrid_newrow_invoke( )
      {
         nRC_GXsfl_9 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_9"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_9_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_9_idx = GetPar( "sGXsfl_9_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrTaggrid_newrow( ) ;
         /* End function gxnrTaggrid_newrow_invoke */
      }

      protected void gxgrTaggrid_refresh_invoke( )
      {
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrTaggrid_refresh( sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrTaggrid_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               ValidateSpaRequest();
            }
            PA2I2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtltagid_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtltagid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtltagid_Enabled), 5, 0), !bGXsfl_9_Refreshing);
               edtavCtlname_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlname_Enabled), 5, 0), !bGXsfl_9_Refreshing);
               WS2I2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  if ( nDynComponent == 0 )
                  {
                     throw new System.Net.WebException("WebComponent is not allowed to run") ;
                  }
               }
            }
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

      protected void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            context.WriteHtmlText( "<title>") ;
            context.SendWebValue( "Tags WC") ;
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
         }
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 310420), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 310420), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 310420), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
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
            bodyStyle = "";
            if ( nGXWrapped == 0 )
            {
               bodyStyle += "-moz-opacity:0;opacity:0;";
            }
            context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.tagswc", new object[] {UrlEncode(AV18groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal Form", true);
         }
         else
         {
            bool toggleHtmlOutput = isOutputEnabled( );
            if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
            }
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            if ( toggleHtmlOutput )
            {
               if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableOutput();
                  }
               }
            }
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Password_tags", AV11Password_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Password_tags", AV11Password_tags);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Edittag", AV8editTag);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Edittag", AV8editTag);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_9", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_9), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV18groupId", wcpOAV18groupId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vPASSWORD_TAGS", AV11Password_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vPASSWORD_TAGS", AV11Password_tags);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vPASSWORDS_AND_TAGS", AV13Passwords_and_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vPASSWORDS_AND_TAGS", AV13Passwords_and_tags);
         }
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vUSERRESPONSE", AV14UserResponse);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vPASSWORDS", AV12Passwords);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vPASSWORDS", AV12Passwords);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vGROUPID", AV18groupId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vEDITTAG", AV8editTag);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vEDITTAG", AV8editTag);
         }
      }

      protected void RenderHtmlCloseForm2I2( )
      {
         SendCloseFormHiddens( ) ;
         if ( ( StringUtil.Len( sPrefix) != 0 ) && ( context.isAjaxRequest( ) || context.isSpaRequest( ) ) )
         {
            componentjscripts();
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GX_FocusControl", GX_FocusControl);
         define_styles( ) ;
         SendSecurityToken(sPrefix);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            SendAjaxEncryptionKey();
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
            context.WriteHtmlTextNl( "</body>") ;
            context.WriteHtmlTextNl( "</html>") ;
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
         }
         else
         {
            SendWebComponentState();
            context.WriteHtmlText( "</div>") ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
      }

      public override string GetPgmname( )
      {
         return "Wallet.TagsWC" ;
      }

      public override string GetPgmdesc( )
      {
         return "Tags WC" ;
      }

      protected void WB2I0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               RenderHtmlHeaders( ) ;
            }
            RenderHtmlOpenForm( ) ;
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.tagswc");
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 6,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttAddatag_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(9), 1, 0)+","+"null"+");", "Add a Tag", bttAddatag_Jsonclick, 7, "Add a Tag", "", StyleString, ClassString, bttAddatag_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e112i1_client"+"'", TempTags, "", 2, "HLP_Wallet/TagsWC.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            TaggridContainer.SetWrapped(nGXWrapped);
            StartGridControl9( ) ;
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            nRC_GXsfl_9 = (int)(nGXsfl_9_idx-1);
            if ( TaggridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV21GXV1 = nGXsfl_9_idx;
               if ( subTaggrid_Visible != 0 )
               {
                  sStyleString = "";
               }
               else
               {
                  sStyleString = " style=\"display:none;\"";
               }
               context.WriteHtmlText( "<div id=\""+sPrefix+"TaggridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Taggrid", TaggridContainer, subTaggrid_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"TaggridContainerData", TaggridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"TaggridContainerData"+"V", TaggridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"TaggridContainerData"+"V"+"\" value='"+TaggridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavCtltagname_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtltagname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtltagname_Internalname, "Name", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtltagname_Internalname, StringUtil.RTrim( AV8editTag.gxTpr_Name), StringUtil.RTrim( context.localUtil.Format( AV8editTag.gxTpr_Name, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtltagname_Jsonclick, 0, "Attribute", "", "", "", "", edtavCtltagname_Visible, edtavCtltagname_Enabled, 0, "text", "", 80, "chr", 1, "row", 80, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/TagsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSave_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(9), 1, 0)+","+"null"+");", "Save", bttSave_Jsonclick, 5, "Save", "", StyleString, ClassString, bttSave_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SAVE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/TagsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 23,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(9), 1, 0)+","+"null"+");", "Cancel", bttCancel_Jsonclick, 7, "Cancel", "", StyleString, ClassString, bttCancel_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e122i1_client"+"'", TempTags, "", 2, "HLP_Wallet/TagsWC.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( TaggridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV21GXV1 = nGXsfl_9_idx;
                  if ( subTaggrid_Visible != 0 )
                  {
                     sStyleString = "";
                  }
                  else
                  {
                     sStyleString = " style=\"display:none;\"";
                  }
                  context.WriteHtmlText( "<div id=\""+sPrefix+"TaggridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Taggrid", TaggridContainer, subTaggrid_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"TaggridContainerData", TaggridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"TaggridContainerData"+"V", TaggridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"TaggridContainerData"+"V"+"\" value='"+TaggridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START2I2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.isSpaRequest( ) )
            {
               if ( context.ExposeMetadata( ) )
               {
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_13-186676", 0) ;
               }
            }
            Form.Meta.addItem("description", "Tags WC", 0) ;
            context.wjLoc = "";
            context.nUserReturn = 0;
            context.wbHandled = 0;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               sXEvt = cgiGet( "_EventName");
               if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
               {
               }
            }
         }
         wbErr = false;
         if ( ( StringUtil.Len( sPrefix) == 0 ) || ( nDraw == 1 ) )
         {
            if ( nDoneStart == 0 )
            {
               STRUP2I0( ) ;
            }
         }
      }

      protected void WS2I2( )
      {
         START2I2( ) ;
         EVT2I2( ) ;
      }

      protected void EVT2I2( )
      {
         sXEvt = cgiGet( "_EventName");
         if ( ( ( ( StringUtil.Len( sPrefix) == 0 ) ) || ( StringUtil.StringSearch( sXEvt, sPrefix, 1) > 0 ) ) && ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               if ( context.wbHandled == 0 )
               {
                  if ( StringUtil.Len( sPrefix) == 0 )
                  {
                     sEvt = cgiGet( "_EventName");
                     EvtGridId = cgiGet( "_EventGridId");
                     EvtRowId = cgiGet( "_EventRowId");
                  }
                  if ( StringUtil.Len( sEvt) > 0 )
                  {
                     sEvtType = StringUtil.Left( sEvt, 1);
                     sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Save' */
                                    E132I2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E142I2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtltagid_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "TAGGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 10), "'EDIT TAB'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'DELETE TAB'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 10), "'EDIT TAB'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'DELETE TAB'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2I0( ) ;
                              }
                              nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
                              SubsflControlProps_92( ) ;
                              AV21GXV1 = nGXsfl_9_idx;
                              if ( ( AV11Password_tags.Count >= AV21GXV1 ) && ( AV21GXV1 > 0 ) )
                              {
                                 AV11Password_tags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV21GXV1));
                                 AV7editImage = cgiGet( edtavEditimage_Internalname);
                                 AssignProp(sPrefix, false, edtavEditimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV7editImage)) ? AV26Editimage_GXI : context.convertURL( context.PathToRelativeUrl( AV7editImage))), !bGXsfl_9_Refreshing);
                                 AssignProp(sPrefix, false, edtavEditimage_Internalname, "SrcSet", context.GetImageSrcSet( AV7editImage), true);
                                 AV6deleteImage = cgiGet( edtavDeleteimage_Internalname);
                                 AssignProp(sPrefix, false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV6deleteImage)) ? AV25Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV6deleteImage))), !bGXsfl_9_Refreshing);
                                 AssignProp(sPrefix, false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV6deleteImage), true);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtltagid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E152I2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "TAGGRID.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtltagid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Taggrid.Load */
                                          E162I2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'EDIT TAB'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtltagid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'edit tab' */
                                          E172I2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DELETE TAB'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtltagid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'delete tab' */
                                          E182I2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          if ( ! wbErr )
                                          {
                                             Rfr0gs = false;
                                             if ( ! Rfr0gs )
                                             {
                                             }
                                             dynload_actions( ) ;
                                          }
                                       }
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                    {
                                       STRUP2I0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtltagid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                       }
                                    }
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

      protected void WE2I2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm2I2( ) ;
            }
         }
      }

      protected void PA2I2( )
      {
         if ( nDonePA == 0 )
         {
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               initialize_properties( ) ;
            }
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
            }
            init_web_controls( ) ;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = edtavCtltagname_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrTaggrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_92( ) ;
         while ( nGXsfl_9_idx <= nRC_GXsfl_9 )
         {
            sendrow_92( ) ;
            nGXsfl_9_idx = ((subTaggrid_Islastpage==1)&&(nGXsfl_9_idx+1>subTaggrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( TaggridContainer)) ;
         /* End function gxnrTaggrid_newrow */
      }

      protected void gxgrTaggrid_refresh( string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         TAGGRID_nCurrentRecord = 0;
         RF2I2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrTaggrid_refresh */
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
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF2I2( ) ;
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

      protected void RF2I2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            TaggridContainer.ClearRows();
         }
         wbStart = 9;
         nGXsfl_9_idx = 1;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         bGXsfl_9_Refreshing = true;
         TaggridContainer.AddObjectProperty("GridName", "Taggrid");
         TaggridContainer.AddObjectProperty("CmpContext", sPrefix);
         TaggridContainer.AddObjectProperty("InMasterPage", "false");
         TaggridContainer.AddObjectProperty("Class", "Grid");
         TaggridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         TaggridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         TaggridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Backcolorstyle), 1, 0, ".", "")));
         TaggridContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Visible), 5, 0, ".", "")));
         TaggridContainer.PageSize = subTaggrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_92( ) ;
            /* Execute user event: Taggrid.Load */
            E162I2 ();
            wbEnd = 9;
            WB2I0( ) ;
         }
         bGXsfl_9_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2I2( )
      {
      }

      protected int subTaggrid_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subTaggrid_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subTaggrid_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subTaggrid_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtltagid_Enabled = 0;
         edtavCtlname_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2I0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E152I2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vEDITTAG"), AV8editTag);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Password_tags"), AV11Password_tags);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Edittag"), AV8editTag);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vPASSWORD_TAGS"), AV11Password_tags);
            /* Read saved values. */
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            wcpOAV18groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV18groupId"));
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_9_fel_idx = 0;
            while ( nGXsfl_9_fel_idx < nRC_GXsfl_9 )
            {
               nGXsfl_9_fel_idx = ((subTaggrid_Islastpage==1)&&(nGXsfl_9_fel_idx+1>subTaggrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_fel_idx+1);
               sGXsfl_9_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_92( ) ;
               AV21GXV1 = nGXsfl_9_fel_idx;
               if ( ( AV11Password_tags.Count >= AV21GXV1 ) && ( AV21GXV1 > 0 ) )
               {
                  AV11Password_tags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV21GXV1));
                  AV7editImage = cgiGet( edtavEditimage_Internalname);
                  AV6deleteImage = cgiGet( edtavDeleteimage_Internalname);
               }
            }
            if ( nGXsfl_9_fel_idx == 0 )
            {
               nGXsfl_9_idx = 1;
               sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
               SubsflControlProps_92( ) ;
            }
            nGXsfl_9_fel_idx = 1;
            /* Read variables values. */
            AV8editTag.gxTpr_Name = cgiGet( edtavCtltagname_Internalname);
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
         E152I2 ();
         if (returnInSub) return;
      }

      protected void E152I2( )
      {
         /* Start Routine */
         returnInSub = false;
         if ( ! (Guid.Empty==AV18groupId) )
         {
            GXt_SdtGroup_SDT1 = AV17group_sdt;
            new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV18groupId, out  GXt_SdtGroup_SDT1) ;
            AV17group_sdt = GXt_SdtGroup_SDT1;
            AV15websession.Set("Group_EDIT", AV17group_sdt.ToJSonString(false, true));
         }
         else
         {
            AV15websession.Set("Group_EDIT", "");
         }
         /* Execute user subroutine: 'READ PASSWORDS_AND_TAGS' */
         S112 ();
         if (returnInSub) return;
         AV12Passwords = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV13Passwords_and_tags.gxTpr_Password.Clone());
         AV11Password_tags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV13Passwords_and_tags.gxTpr_Password_tag.Clone());
         gx_BV9 = true;
         edtavCtltagname_Visible = 0;
         AssignProp(sPrefix, false, edtavCtltagname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCtltagname_Visible), 5, 0), true);
         bttSave_Visible = 0;
         AssignProp(sPrefix, false, bttSave_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSave_Visible), 5, 0), true);
         bttCancel_Visible = 0;
         AssignProp(sPrefix, false, bttCancel_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCancel_Visible), 5, 0), true);
      }

      private void E162I2( )
      {
         /* Taggrid_Load Routine */
         returnInSub = false;
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV11Password_tags.Count )
         {
            AV11Password_tags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV21GXV1));
            edtavDeleteimage_gximage = "GeneXusUnanimo_delete_light";
            AV6deleteImage = context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavDeleteimage_Internalname, AV6deleteImage);
            AV25Deleteimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( )), context);
            edtavEditimage_gximage = "GeneXusUnanimo_edit_light";
            AV7editImage = context.GetImagePath( "f20f0a60-bf65-4ff0-a2d0-9392c759340b", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavEditimage_Internalname, AV7editImage);
            AV26Editimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "f20f0a60-bf65-4ff0-a2d0-9392c759340b", "", context.GetTheme( )), context);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 9;
            }
            sendrow_92( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_9_Refreshing )
            {
               DoAjaxLoad(9, TaggridRow);
            }
            AV21GXV1 = (int)(AV21GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E132I2( )
      {
         AV21GXV1 = nGXsfl_9_idx;
         if ( ( AV21GXV1 > 0 ) && ( AV11Password_tags.Count >= AV21GXV1 ) )
         {
            AV11Password_tags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV21GXV1));
         }
         /* 'Save' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8editTag.gxTpr_Name)) )
         {
            GX_msglist.addItem("Please enter a Name for the tag");
         }
         else
         {
            AV10oneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
            AV10oneTag.gxTpr_Name = StringUtil.Trim( AV8editTag.gxTpr_Name);
            if ( (Guid.Empty==AV8editTag.gxTpr_Tagid) )
            {
               AV10oneTag.gxTpr_Tagid = Guid.NewGuid( );
            }
            else
            {
               AV10oneTag.gxTpr_Tagid = AV8editTag.gxTpr_Tagid;
               AV27GXV5 = 1;
               while ( AV27GXV5 <= AV11Password_tags.Count )
               {
                  AV5findTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV27GXV5));
                  if ( AV5findTag.gxTpr_Tagid == AV8editTag.gxTpr_Tagid )
                  {
                     AV11Password_tags.RemoveItem(AV11Password_tags.IndexOf(AV5findTag));
                     gx_BV9 = true;
                  }
                  AV27GXV5 = (int)(AV27GXV5+1);
               }
            }
            AV11Password_tags.Add(AV10oneTag, 0);
            gx_BV9 = true;
            AV13Passwords_and_tags.gxTpr_Password_tag = AV11Password_tags;
            /* Execute user subroutine: 'SAVE PASSWORDS_AND_TAGS' */
            S122 ();
            if (returnInSub) return;
            subTaggrid_Visible = 1;
            AssignProp(sPrefix, false, sPrefix+"TaggridContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subTaggrid_Visible), 5, 0), true);
            bttAddatag_Visible = 1;
            AssignProp(sPrefix, false, bttAddatag_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddatag_Visible), 5, 0), true);
            edtavCtltagname_Visible = 0;
            AssignProp(sPrefix, false, edtavCtltagname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCtltagname_Visible), 5, 0), true);
            bttSave_Visible = 0;
            AssignProp(sPrefix, false, bttSave_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSave_Visible), 5, 0), true);
            bttCancel_Visible = 0;
            AssignProp(sPrefix, false, bttCancel_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCancel_Visible), 5, 0), true);
            AV15websession.Set("ONE_PASSWORD_TO_ENCR", "");
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "DoneWithPassword", new Object[] {(bool)false}, true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11Password_tags", AV11Password_tags);
         nGXsfl_9_bak_idx = nGXsfl_9_idx;
         gxgrTaggrid_refresh( sPrefix) ;
         nGXsfl_9_idx = nGXsfl_9_bak_idx;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13Passwords_and_tags", AV13Passwords_and_tags);
      }

      protected void E172I2( )
      {
         AV21GXV1 = nGXsfl_9_idx;
         if ( ( AV21GXV1 > 0 ) && ( AV11Password_tags.Count >= AV21GXV1 ) )
         {
            AV11Password_tags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV21GXV1));
         }
         /* 'edit tab' Routine */
         returnInSub = false;
         AV8editTag = (GeneXus.Programs.wallet.SdtPassword_tag)(((GeneXus.Programs.wallet.SdtPassword_tag)(AV11Password_tags.CurrentItem)).Clone());
         subTaggrid_Visible = 0;
         AssignProp(sPrefix, false, sPrefix+"TaggridContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subTaggrid_Visible), 5, 0), true);
         bttAddatag_Visible = 0;
         AssignProp(sPrefix, false, bttAddatag_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddatag_Visible), 5, 0), true);
         edtavCtltagname_Visible = 1;
         AssignProp(sPrefix, false, edtavCtltagname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCtltagname_Visible), 5, 0), true);
         bttSave_Visible = 1;
         AssignProp(sPrefix, false, bttSave_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSave_Visible), 5, 0), true);
         bttCancel_Visible = 1;
         AssignProp(sPrefix, false, bttCancel_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCancel_Visible), 5, 0), true);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV8editTag", AV8editTag);
      }

      protected void E182I2( )
      {
         AV21GXV1 = nGXsfl_9_idx;
         if ( ( AV21GXV1 > 0 ) && ( AV11Password_tags.Count >= AV21GXV1 ) )
         {
            AV11Password_tags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV21GXV1));
         }
         /* 'delete tab' Routine */
         returnInSub = false;
         this.executeExternalObjectMethod(sPrefix, false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to Delete "+StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword_tag)(AV11Password_tags.CurrentItem)).gxTpr_Name)+" Tag?"}, false);
      }

      protected void E142I2( )
      {
         AV21GXV1 = nGXsfl_9_idx;
         if ( ( AV21GXV1 > 0 ) && ( AV11Password_tags.Count >= AV21GXV1 ) )
         {
            AV11Password_tags.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV21GXV1));
         }
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV14UserResponse )
         {
            AV28GXV6 = 1;
            while ( AV28GXV6 <= AV11Password_tags.Count )
            {
               AV5findTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV28GXV6));
               if ( AV5findTag.gxTpr_Tagid == ((GeneXus.Programs.wallet.SdtPassword_tag)(AV11Password_tags.CurrentItem)).gxTpr_Tagid )
               {
                  AV11Password_tags.RemoveItem(AV11Password_tags.IndexOf(AV5findTag));
                  gx_BV9 = true;
               }
               AV28GXV6 = (int)(AV28GXV6+1);
            }
            AV13Passwords_and_tags.gxTpr_Password_tag = AV11Password_tags;
            AV29GXV7 = 1;
            while ( AV29GXV7 <= AV12Passwords.Count )
            {
               AV19onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV12Passwords.Item(AV29GXV7));
               AV30GXV8 = 1;
               while ( AV30GXV8 <= AV19onePassword.gxTpr_Password_tag.Count )
               {
                  AV5findTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV19onePassword.gxTpr_Password_tag.Item(AV30GXV8));
                  if ( AV5findTag.gxTpr_Tagid == ((GeneXus.Programs.wallet.SdtPassword_tag)(AV11Password_tags.CurrentItem)).gxTpr_Tagid )
                  {
                     AV19onePassword.gxTpr_Password_tag.RemoveItem(AV19onePassword.gxTpr_Password_tag.IndexOf(AV5findTag));
                  }
                  AV30GXV8 = (int)(AV30GXV8+1);
               }
               AV29GXV7 = (int)(AV29GXV7+1);
            }
            AV13Passwords_and_tags.gxTpr_Password = AV12Passwords;
            /* Execute user subroutine: 'SAVE PASSWORDS_AND_TAGS' */
            S122 ();
            if (returnInSub) return;
            AV15websession.Set("ONE_PASSWORD_TO_ENCR", "");
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "DoneWithPassword", new Object[] {(bool)false}, true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11Password_tags", AV11Password_tags);
         nGXsfl_9_bak_idx = nGXsfl_9_idx;
         gxgrTaggrid_refresh( sPrefix) ;
         nGXsfl_9_idx = nGXsfl_9_bak_idx;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13Passwords_and_tags", AV13Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV12Passwords", AV12Passwords);
      }

      protected void S112( )
      {
         /* 'READ PASSWORDS_AND_TAGS' Routine */
         returnInSub = false;
         AV17group_sdt.FromJSonString(AV15websession.Get("Group_EDIT"), null);
         if ( AV17group_sdt.gxTpr_Grouptype == 40 )
         {
            GXt_char2 = AV9error;
            new GeneXus.Programs.distributedcryptographylib.decryptjson(context ).execute(  AV17group_sdt.gxTpr_Encryptedtextshare,  AV17group_sdt.gxTpr_Encpassword, out  AV16clearText, out  GXt_char2) ;
            AV9error = GXt_char2;
            AssignAttri(sPrefix, false, "AV9error", AV9error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               if ( StringUtil.StrCmp(AV16clearText, "_empty_") == 0 )
               {
                  AV16clearText = "";
               }
               AV13Passwords_and_tags.FromJSonString(AV16clearText, null);
            }
            else
            {
               GX_msglist.addItem(AV9error);
            }
         }
         else
         {
            AV13Passwords_and_tags.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "encpasswords.enc", out  AV9error), null);
         }
      }

      protected void S122( )
      {
         /* 'SAVE PASSWORDS_AND_TAGS' Routine */
         returnInSub = false;
         AV17group_sdt.FromJSonString(AV15websession.Get("Group_EDIT"), null);
         if ( AV17group_sdt.gxTpr_Grouptype == 40 )
         {
            if ( AV17group_sdt.gxTpr_Amigroupowner )
            {
               GXt_char2 = AV9error;
               GXt_char3 = AV17group_sdt.gxTpr_Encpassword;
               GXt_char4 = AV17group_sdt.gxTpr_Encryptedtextshare;
               new GeneXus.Programs.distributedcryptographylib.encryptjson(context ).execute(  AV13Passwords_and_tags.ToJSonString(false, true),  "", out  GXt_char3, out  GXt_char4, out  GXt_char2) ;
               AV17group_sdt.gxTpr_Encpassword = GXt_char3;
               AV17group_sdt.gxTpr_Encryptedtextshare = GXt_char4;
               AV9error = GXt_char2;
               AssignAttri(sPrefix, false, "AV9error", AV9error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  GXt_char4 = AV9error;
                  new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV17group_sdt,  StringUtil.Trim( AV17group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV20returnGroupId, out  GXt_char4) ;
                  AV9error = GXt_char4;
                  AssignAttri(sPrefix, false, "AV9error", AV9error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
                  {
                     GXt_char4 = AV9error;
                     new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV17group_sdt, out  GXt_char4) ;
                     AV9error = GXt_char4;
                     AssignAttri(sPrefix, false, "AV9error", AV9error);
                  }
                  else
                  {
                     GX_msglist.addItem("There was an error updating group on server: "+AV9error);
                  }
               }
               else
               {
                  GX_msglist.addItem("There was an error encrypting Password: "+AV9error);
               }
            }
         }
         else
         {
            GXt_char4 = AV9error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "encpasswords.enc",  AV13Passwords_and_tags.ToJSonString(false, true), out  GXt_char4) ;
            AV9error = GXt_char4;
            AssignAttri(sPrefix, false, "AV9error", AV9error);
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV18groupId = (Guid)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV18groupId", AV18groupId.ToString());
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
         PA2I2( ) ;
         WS2I2( ) ;
         WE2I2( ) ;
         cleanup();
         context.SetWrapped(false);
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      public override void componentbind( Object[] obj )
      {
         if ( IsUrlCreated( ) )
         {
            return  ;
         }
         sCtrlAV18groupId = (string)((string)getParm(obj,0));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA2I2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\tagswc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA2I2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV18groupId = (Guid)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV18groupId", AV18groupId.ToString());
         }
         wcpOAV18groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV18groupId"));
         if ( ! GetJustCreated( ) && ( ( AV18groupId != wcpOAV18groupId ) ) )
         {
            setjustcreated();
         }
         wcpOAV18groupId = AV18groupId;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV18groupId = cgiGet( sPrefix+"AV18groupId_CTRL");
         if ( StringUtil.Len( sCtrlAV18groupId) > 0 )
         {
            AV18groupId = StringUtil.StrToGuid( cgiGet( sCtrlAV18groupId));
            AssignAttri(sPrefix, false, "AV18groupId", AV18groupId.ToString());
         }
         else
         {
            AV18groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"AV18groupId_PARM"));
         }
      }

      public override void componentprocess( string sPPrefix ,
                                             string sPSFPrefix ,
                                             string sCompEvt )
      {
         sCompPrefix = sPPrefix;
         sSFPrefix = sPSFPrefix;
         sPrefix = sCompPrefix + sSFPrefix;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         INITWEB( ) ;
         nDraw = 0;
         PA2I2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS2I2( ) ;
         if ( isFullAjaxMode( ) )
         {
            componentdraw();
         }
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override void componentstart( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
      }

      protected void WCStart( )
      {
         nDraw = 1;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WS2I2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV18groupId_PARM", AV18groupId.ToString());
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV18groupId)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV18groupId_CTRL", StringUtil.RTrim( sCtrlAV18groupId));
         }
      }

      public override void componentdraw( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WCParametersSet( ) ;
         WE2I2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override string getstring( string sGXControl )
      {
         string sCtrlName;
         if ( StringUtil.StrCmp(StringUtil.Substring( sGXControl, 1, 1), "&") == 0 )
         {
            sCtrlName = StringUtil.Substring( sGXControl, 2, StringUtil.Len( sGXControl)-1);
         }
         else
         {
            sCtrlName = sGXControl;
         }
         return cgiGet( sPrefix+"v"+StringUtil.Upper( sCtrlName)) ;
      }

      public override void componentjscripts( )
      {
         include_jscripts( ) ;
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20257241621502", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         CloseStyles();
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("wallet/tagswc.js", "?20257241621502", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_92( )
      {
         edtavCtltagid_Internalname = sPrefix+"CTLTAGID_"+sGXsfl_9_idx;
         edtavCtlname_Internalname = sPrefix+"CTLNAME_"+sGXsfl_9_idx;
         edtavEditimage_Internalname = sPrefix+"vEDITIMAGE_"+sGXsfl_9_idx;
         edtavDeleteimage_Internalname = sPrefix+"vDELETEIMAGE_"+sGXsfl_9_idx;
      }

      protected void SubsflControlProps_fel_92( )
      {
         edtavCtltagid_Internalname = sPrefix+"CTLTAGID_"+sGXsfl_9_fel_idx;
         edtavCtlname_Internalname = sPrefix+"CTLNAME_"+sGXsfl_9_fel_idx;
         edtavEditimage_Internalname = sPrefix+"vEDITIMAGE_"+sGXsfl_9_fel_idx;
         edtavDeleteimage_Internalname = sPrefix+"vDELETEIMAGE_"+sGXsfl_9_fel_idx;
      }

      protected void sendrow_92( )
      {
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         WB2I0( ) ;
         TaggridRow = GXWebRow.GetNew(context,TaggridContainer);
         if ( subTaggrid_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subTaggrid_Backstyle = 0;
            if ( StringUtil.StrCmp(subTaggrid_Class, "") != 0 )
            {
               subTaggrid_Linesclass = subTaggrid_Class+"Odd";
            }
         }
         else if ( subTaggrid_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subTaggrid_Backstyle = 0;
            subTaggrid_Backcolor = subTaggrid_Allbackcolor;
            if ( StringUtil.StrCmp(subTaggrid_Class, "") != 0 )
            {
               subTaggrid_Linesclass = subTaggrid_Class+"Uniform";
            }
         }
         else if ( subTaggrid_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subTaggrid_Backstyle = 1;
            if ( StringUtil.StrCmp(subTaggrid_Class, "") != 0 )
            {
               subTaggrid_Linesclass = subTaggrid_Class+"Odd";
            }
            subTaggrid_Backcolor = (int)(0x0);
         }
         else if ( subTaggrid_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subTaggrid_Backstyle = 1;
            if ( ((int)((nGXsfl_9_idx) % (2))) == 0 )
            {
               subTaggrid_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subTaggrid_Class, "") != 0 )
               {
                  subTaggrid_Linesclass = subTaggrid_Class+"Even";
               }
            }
            else
            {
               subTaggrid_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subTaggrid_Class, "") != 0 )
               {
                  subTaggrid_Linesclass = subTaggrid_Class+"Odd";
               }
            }
         }
         if ( TaggridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_9_idx+"\">") ;
         }
         /* Subfile cell */
         if ( TaggridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         TaggridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtltagid_Internalname,((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV21GXV1)).gxTpr_Tagid.ToString(),((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV21GXV1)).gxTpr_Tagid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtltagid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtltagid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)9,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( TaggridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         TaggridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlname_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtPassword_tag)AV11Password_tags.Item(AV21GXV1)).gxTpr_Name),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,11);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlname_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlname_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)80,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( TaggridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'" + sPrefix + "',false,'',9)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavEditimage_gximage, "")==0) ? "" : "GX_Image_"+edtavEditimage_gximage+"_Class");
         StyleString = "";
         AV7editImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV7editImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV26Editimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV7editImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV7editImage)) ? AV26Editimage_GXI : context.PathToRelativeUrl( AV7editImage));
         TaggridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavEditimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"edit",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavEditimage_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'EDIT TAB\\'."+sGXsfl_9_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV7editImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         /* Subfile cell */
         if ( TaggridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'" + sPrefix + "',false,'',9)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class");
         StyleString = "";
         AV6deleteImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV6deleteImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV25Deleteimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV6deleteImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV6deleteImage)) ? AV25Deleteimage_GXI : context.PathToRelativeUrl( AV6deleteImage));
         TaggridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"delete",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavDeleteimage_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DELETE TAB\\'."+sGXsfl_9_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV6deleteImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         send_integrity_lvl_hashes2I2( ) ;
         TaggridContainer.AddRow(TaggridRow);
         nGXsfl_9_idx = ((subTaggrid_Islastpage==1)&&(nGXsfl_9_idx+1>subTaggrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         /* End function sendrow_92 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl9( )
      {
         if ( TaggridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"TaggridContainer"+"DivS\" data-gxgridid=\"9\">") ;
            sStyleString = "";
            if ( subTaggrid_Visible == 0 )
            {
               sStyleString += "display:none;";
            }
            GxWebStd.gx_table_start( context, subTaggrid_Internalname, subTaggrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subTaggrid_Backcolorstyle == 0 )
            {
               subTaggrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subTaggrid_Class) > 0 )
               {
                  subTaggrid_Linesclass = subTaggrid_Class+"Title";
               }
            }
            else
            {
               subTaggrid_Titlebackstyle = 1;
               if ( subTaggrid_Backcolorstyle == 1 )
               {
                  subTaggrid_Titlebackcolor = subTaggrid_Allbackcolor;
                  if ( StringUtil.Len( subTaggrid_Class) > 0 )
                  {
                     subTaggrid_Linesclass = subTaggrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subTaggrid_Class) > 0 )
                  {
                     subTaggrid_Linesclass = subTaggrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavEditimage_gximage, "")==0) ? "" : "GX_Image_"+edtavEditimage_gximage+"_Class")+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class")+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            TaggridContainer.AddObjectProperty("GridName", "Taggrid");
         }
         else
         {
            TaggridContainer.AddObjectProperty("GridName", "Taggrid");
            TaggridContainer.AddObjectProperty("Header", subTaggrid_Header);
            TaggridContainer.AddObjectProperty("Class", "Grid");
            TaggridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            TaggridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            TaggridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Backcolorstyle), 1, 0, ".", "")));
            TaggridContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Visible), 5, 0, ".", "")));
            TaggridContainer.AddObjectProperty("CmpContext", sPrefix);
            TaggridContainer.AddObjectProperty("InMasterPage", "false");
            TaggridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TaggridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtltagid_Enabled), 5, 0, ".", "")));
            TaggridContainer.AddColumnProperties(TaggridColumn);
            TaggridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TaggridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlname_Enabled), 5, 0, ".", "")));
            TaggridContainer.AddColumnProperties(TaggridColumn);
            TaggridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TaggridColumn.AddObjectProperty("Value", context.convertURL( AV7editImage));
            TaggridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavEditimage_Link));
            TaggridContainer.AddColumnProperties(TaggridColumn);
            TaggridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TaggridColumn.AddObjectProperty("Value", context.convertURL( AV6deleteImage));
            TaggridContainer.AddColumnProperties(TaggridColumn);
            TaggridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Selectedindex), 4, 0, ".", "")));
            TaggridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Allowselection), 1, 0, ".", "")));
            TaggridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Selectioncolor), 9, 0, ".", "")));
            TaggridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Allowhovering), 1, 0, ".", "")));
            TaggridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Hoveringcolor), 9, 0, ".", "")));
            TaggridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Allowcollapsing), 1, 0, ".", "")));
            TaggridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTaggrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         bttAddatag_Internalname = sPrefix+"ADDATAG";
         edtavCtltagid_Internalname = sPrefix+"CTLTAGID";
         edtavCtlname_Internalname = sPrefix+"CTLNAME";
         edtavEditimage_Internalname = sPrefix+"vEDITIMAGE";
         edtavDeleteimage_Internalname = sPrefix+"vDELETEIMAGE";
         edtavCtltagname_Internalname = sPrefix+"CTLTAGNAME";
         bttSave_Internalname = sPrefix+"SAVE";
         bttCancel_Internalname = sPrefix+"CANCEL";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subTaggrid_Internalname = sPrefix+"TAGGRID";
      }

      public override void initialize_properties( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         init_default_properties( ) ;
         subTaggrid_Allowcollapsing = 0;
         subTaggrid_Allowselection = 0;
         edtavEditimage_Link = "";
         subTaggrid_Header = "";
         edtavDeleteimage_Jsonclick = "";
         edtavDeleteimage_gximage = "";
         edtavEditimage_Jsonclick = "";
         edtavEditimage_gximage = "";
         edtavCtlname_Jsonclick = "";
         edtavCtlname_Enabled = 0;
         edtavCtltagid_Jsonclick = "";
         edtavCtltagid_Enabled = 0;
         subTaggrid_Class = "Grid";
         subTaggrid_Backcolorstyle = 0;
         bttCancel_Visible = 1;
         bttSave_Visible = 1;
         edtavCtltagname_Jsonclick = "";
         edtavCtltagname_Enabled = 1;
         edtavCtltagname_Visible = 1;
         subTaggrid_Visible = 1;
         bttAddatag_Visible = 1;
         edtavCtlname_Enabled = -1;
         edtavCtltagid_Enabled = -1;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"TAGGRID_nFirstRecordOnPage","type":"int"},{"av":"TAGGRID_nEOF","type":"int"},{"av":"AV11Password_tags","fld":"vPASSWORD_TAGS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"TAGGRID","prop":"GridRC","grid":9,"type":"int"},{"av":"sPrefix","type":"char"}]}""");
         setEventMetadata("'ADD A TAG'","""{"handler":"E112I1","iparms":[]""");
         setEventMetadata("'ADD A TAG'",""","oparms":[{"av":"AV8editTag","fld":"vEDITTAG","type":""},{"av":"subTaggrid_Visible","ctrl":"TAGGRID","prop":"Visible"},{"ctrl":"ADDATAG","prop":"Visible"},{"ctrl":"CTLTAGNAME","prop":"Visible"},{"ctrl":"SAVE","prop":"Visible"},{"ctrl":"CANCEL","prop":"Visible"}]}""");
         setEventMetadata("TAGGRID.LOAD","""{"handler":"E162I2","iparms":[]""");
         setEventMetadata("TAGGRID.LOAD",""","oparms":[{"av":"AV6deleteImage","fld":"vDELETEIMAGE","type":"bits"},{"av":"AV7editImage","fld":"vEDITIMAGE","type":"bits"}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E122I1","iparms":[]""");
         setEventMetadata("'CANCEL'",""","oparms":[{"av":"subTaggrid_Visible","ctrl":"TAGGRID","prop":"Visible"},{"ctrl":"ADDATAG","prop":"Visible"},{"ctrl":"CTLTAGNAME","prop":"Visible"},{"ctrl":"SAVE","prop":"Visible"},{"ctrl":"CANCEL","prop":"Visible"}]}""");
         setEventMetadata("'SAVE'","""{"handler":"E132I2","iparms":[{"av":"AV8editTag","fld":"vEDITTAG","type":""},{"av":"AV11Password_tags","fld":"vPASSWORD_TAGS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"TAGGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"TAGGRID","prop":"GridRC","grid":9,"type":"int"},{"av":"AV13Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"TAGGRID_nEOF","type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'SAVE'",""","oparms":[{"av":"AV11Password_tags","fld":"vPASSWORD_TAGS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"TAGGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"TAGGRID","prop":"GridRC","grid":9,"type":"int"},{"av":"AV13Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"subTaggrid_Visible","ctrl":"TAGGRID","prop":"Visible"},{"ctrl":"ADDATAG","prop":"Visible"},{"ctrl":"CTLTAGNAME","prop":"Visible"},{"ctrl":"SAVE","prop":"Visible"},{"ctrl":"CANCEL","prop":"Visible"},{"av":"AV9error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'EDIT TAB'","""{"handler":"E172I2","iparms":[{"av":"AV11Password_tags","fld":"vPASSWORD_TAGS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"TAGGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"TAGGRID","prop":"GridRC","grid":9,"type":"int"}]""");
         setEventMetadata("'EDIT TAB'",""","oparms":[{"av":"AV8editTag","fld":"vEDITTAG","type":""},{"av":"subTaggrid_Visible","ctrl":"TAGGRID","prop":"Visible"},{"ctrl":"ADDATAG","prop":"Visible"},{"ctrl":"CTLTAGNAME","prop":"Visible"},{"ctrl":"SAVE","prop":"Visible"},{"ctrl":"CANCEL","prop":"Visible"}]}""");
         setEventMetadata("'DELETE TAB'","""{"handler":"E182I2","iparms":[{"av":"AV11Password_tags","fld":"vPASSWORD_TAGS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"TAGGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"TAGGRID","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E142I2","iparms":[{"av":"AV14UserResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV11Password_tags","fld":"vPASSWORD_TAGS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"TAGGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"TAGGRID","prop":"GridRC","grid":9,"type":"int"},{"av":"AV13Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV12Passwords","fld":"vPASSWORDS","type":""},{"av":"TAGGRID_nEOF","type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV11Password_tags","fld":"vPASSWORD_TAGS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"TAGGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"TAGGRID","prop":"GridRC","grid":9,"type":"int"},{"av":"AV13Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV12Passwords","fld":"vPASSWORDS","type":""},{"av":"AV9error","fld":"vERROR","type":"char"}]}""");
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
         wcpOAV18groupId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV11Password_tags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography");
         AV8editTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV13Passwords_and_tags = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         AV12Passwords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "distributedcryptography");
         GX_FocusControl = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttAddatag_Jsonclick = "";
         TaggridContainer = new GXWebGrid( context);
         sStyleString = "";
         bttSave_Jsonclick = "";
         bttCancel_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV7editImage = "";
         AV26Editimage_GXI = "";
         AV6deleteImage = "";
         AV25Deleteimage_GXI = "";
         AV17group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV15websession = context.GetSession();
         TaggridRow = new GXWebRow();
         AV10oneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV5findTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV19onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV9error = "";
         AV16clearText = "";
         GXt_char2 = "";
         GXt_char3 = "";
         AV20returnGroupId = Guid.Empty;
         GXt_char4 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV18groupId = "";
         subTaggrid_Linesclass = "";
         ROClassString = "";
         sImgUrl = "";
         TaggridColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtltagid_Enabled = 0;
         edtavCtlname_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subTaggrid_Backcolorstyle ;
      private short TAGGRID_nEOF ;
      private short nGXWrapped ;
      private short subTaggrid_Backstyle ;
      private short subTaggrid_Titlebackstyle ;
      private short subTaggrid_Allowselection ;
      private short subTaggrid_Allowhovering ;
      private short subTaggrid_Allowcollapsing ;
      private short subTaggrid_Collapsed ;
      private int nRC_GXsfl_9 ;
      private int nGXsfl_9_idx=1 ;
      private int edtavCtltagid_Enabled ;
      private int edtavCtlname_Enabled ;
      private int bttAddatag_Visible ;
      private int AV21GXV1 ;
      private int subTaggrid_Visible ;
      private int edtavCtltagname_Visible ;
      private int edtavCtltagname_Enabled ;
      private int bttSave_Visible ;
      private int bttCancel_Visible ;
      private int subTaggrid_Islastpage ;
      private int nGXsfl_9_fel_idx=1 ;
      private int AV27GXV5 ;
      private int nGXsfl_9_bak_idx=1 ;
      private int AV28GXV6 ;
      private int AV29GXV7 ;
      private int AV30GXV8 ;
      private int idxLst ;
      private int subTaggrid_Backcolor ;
      private int subTaggrid_Allbackcolor ;
      private int subTaggrid_Titlebackcolor ;
      private int subTaggrid_Selectedindex ;
      private int subTaggrid_Selectioncolor ;
      private int subTaggrid_Hoveringcolor ;
      private long TAGGRID_nCurrentRecord ;
      private long TAGGRID_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_9_idx="0001" ;
      private string edtavCtltagid_Internalname ;
      private string edtavCtlname_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttAddatag_Internalname ;
      private string bttAddatag_Jsonclick ;
      private string sStyleString ;
      private string subTaggrid_Internalname ;
      private string edtavCtltagname_Internalname ;
      private string edtavCtltagname_Jsonclick ;
      private string bttSave_Internalname ;
      private string bttSave_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavEditimage_Internalname ;
      private string edtavDeleteimage_Internalname ;
      private string sGXsfl_9_fel_idx="0001" ;
      private string edtavDeleteimage_gximage ;
      private string edtavEditimage_gximage ;
      private string AV9error ;
      private string GXt_char2 ;
      private string GXt_char3 ;
      private string GXt_char4 ;
      private string sCtrlAV18groupId ;
      private string subTaggrid_Class ;
      private string subTaggrid_Linesclass ;
      private string ROClassString ;
      private string edtavCtltagid_Jsonclick ;
      private string edtavCtlname_Jsonclick ;
      private string sImgUrl ;
      private string edtavEditimage_Jsonclick ;
      private string edtavDeleteimage_Jsonclick ;
      private string subTaggrid_Header ;
      private string edtavEditimage_Link ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_9_Refreshing=false ;
      private bool AV14UserResponse ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV9 ;
      private bool AV7editImage_IsBlob ;
      private bool AV6deleteImage_IsBlob ;
      private string AV16clearText ;
      private string AV26Editimage_GXI ;
      private string AV25Deleteimage_GXI ;
      private string AV7editImage ;
      private string AV6deleteImage ;
      private Guid AV18groupId ;
      private Guid wcpOAV18groupId ;
      private Guid AV20returnGroupId ;
      private GXWebGrid TaggridContainer ;
      private GXWebRow TaggridRow ;
      private GXWebColumn TaggridColumn ;
      private GXWebForm Form ;
      private IGxSession AV15websession ;
      private IGxDataStore dsDefault ;
      private Guid aP0_groupId ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV11Password_tags ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV8editTag ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV13Passwords_and_tags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV12Passwords ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV10oneTag ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV5findTag ;
      private GeneXus.Programs.wallet.SdtPassword AV19onePassword ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
