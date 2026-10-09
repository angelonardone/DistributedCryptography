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
namespace GeneXus.Programs.wallet.registered {
   public class legacyallsignatures : GXWebComponent
   {
      public legacyallsignatures( )
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

      public legacyallsignatures( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_groupId )
      {
         this.AV11groupId = aP0_groupId;
         ExecuteImpl();
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
         chkavCtlcompleated = new GXCheckbox();
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
                  AV11groupId = StringUtil.StrToGuid( GetPar( "groupId"));
                  AssignAttri(sPrefix, false, "AV11groupId", AV11groupId.ToString());
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(Guid)AV11groupId});
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridsignatures") == 0 )
               {
                  gxnrGridsignatures_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridsignatures") == 0 )
               {
                  gxgrGridsignatures_refresh_invoke( ) ;
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

      protected void gxnrGridsignatures_newrow_invoke( )
      {
         nRC_GXsfl_6 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_6"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_6_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_6_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_6_idx = GetPar( "sGXsfl_6_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridsignatures_newrow( ) ;
         /* End function gxnrGridsignatures_newrow_invoke */
      }

      protected void gxgrGridsignatures_refresh_invoke( )
      {
         subGridsignatures_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subGridsignatures_Rows"), "."), 18, MidpointRounding.ToEven));
         AV11groupId = StringUtil.StrToGuid( GetPar( "groupId"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV22signatureRows);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridsignatures_refresh( subGridsignatures_Rows, AV11groupId, AV22signatureRows, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridsignatures_refresh_invoke */
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
            PA3D2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtlid_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlid_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavCtldescription_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtldescription_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldescription_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavCtlsigneddatetime_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlsigneddatetime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlsigneddatetime_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavCtlsenderusername_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlsenderusername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlsenderusername_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               chkavCtlcompleated.Enabled = 0;
               AssignProp(sPrefix, false, chkavCtlcompleated_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavCtlcompleated.Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavCtlsendcoins_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlsendcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlsendcoins_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavCtlsendto_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlsendto_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlsendto_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavSign_Enabled = 0;
               AssignProp(sPrefix, false, edtavSign_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSign_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               WS3D2( ) ;
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
            context.SendWebValue( "Legacy All Signatures") ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.legacyallsignatures", new object[] {UrlEncode(AV11groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Signaturerows", AV22signatureRows);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Signaturerows", AV22signatureRows);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_6", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_6), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV11groupId", wcpOAV11groupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"vGROUPID", AV11groupId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vSIGNATUREROWS", AV22signatureRows);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vSIGNATUREROWS", AV22signatureRows);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vPOPUPNAME", StringUtil.RTrim( AV15PopupName));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDSIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDSIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDSIGNATURES_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDSIGNATURES_nEOF), 1, 0, ".", "")));
      }

      protected void RenderHtmlCloseForm3D2( )
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
         return "Wallet.registered.LegacyAllSignatures" ;
      }

      public override string GetPgmdesc( )
      {
         return "Legacy All Signatures" ;
      }

      protected void WB3D0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.legacyallsignatures");
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridsignaturesContainer.SetWrapped(nGXWrapped);
            StartGridControl6( ) ;
         }
         if ( wbEnd == 6 )
         {
            wbEnd = 0;
            nRC_GXsfl_6 = (int)(nGXsfl_6_idx-1);
            if ( GridsignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               GridsignaturesContainer.AddObjectProperty("GRIDSIGNATURES_nEOF", GRIDSIGNATURES_nEOF);
               GridsignaturesContainer.AddObjectProperty("GRIDSIGNATURES_nFirstRecordOnPage", GRIDSIGNATURES_nFirstRecordOnPage);
               AV24GXV1 = nGXsfl_6_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridsignaturesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridsignatures", GridsignaturesContainer, subGridsignatures_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridsignaturesContainerData", GridsignaturesContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridsignaturesContainerData"+"V", GridsignaturesContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridsignaturesContainerData"+"V"+"\" value='"+GridsignaturesContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 6 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridsignaturesContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  GridsignaturesContainer.AddObjectProperty("GRIDSIGNATURES_nEOF", GRIDSIGNATURES_nEOF);
                  GridsignaturesContainer.AddObjectProperty("GRIDSIGNATURES_nFirstRecordOnPage", GRIDSIGNATURES_nFirstRecordOnPage);
                  AV24GXV1 = nGXsfl_6_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridsignaturesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridsignatures", GridsignaturesContainer, subGridsignatures_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridsignaturesContainerData", GridsignaturesContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridsignaturesContainerData"+"V", GridsignaturesContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridsignaturesContainerData"+"V"+"\" value='"+GridsignaturesContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3D2( )
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
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
               }
            }
            Form.Meta.addItem("description", "Legacy All Signatures", 0) ;
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
               STRUP3D0( ) ;
            }
         }
      }

      protected void WS3D2( )
      {
         START3D2( ) ;
         EVT3D2( ) ;
      }

      protected void EVT3D2( )
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
                                 STRUP3D0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3D0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E113D2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3D0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtlid_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDSIGNATURESPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3D0( ) ;
                              }
                              sEvt = cgiGet( sPrefix+"GRIDSIGNATURESPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subgridsignatures_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subgridsignatures_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subgridsignatures_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subgridsignatures_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "'SIGN TRANSACTION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 19), "GRIDSIGNATURES.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "'SIGN TRANSACTION'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3D0( ) ;
                              }
                              nGXsfl_6_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
                              SubsflControlProps_62( ) ;
                              AV24GXV1 = (int)(nGXsfl_6_idx+GRIDSIGNATURES_nFirstRecordOnPage);
                              if ( ( AV22signatureRows.Count >= AV24GXV1 ) && ( AV24GXV1 > 0 ) )
                              {
                                 AV22signatureRows.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1));
                                 AV18sign = cgiGet( edtavSign_Internalname);
                                 AssignAttri(sPrefix, false, edtavSign_Internalname, AV18sign);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E123D2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'SIGN TRANSACTION'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Sign Transaction' */
                                          E133D2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDSIGNATURES.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Gridsignatures.Load */
                                          E143D2 ();
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
                                       STRUP3D0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlid_Internalname;
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

      protected void WE3D2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm3D2( ) ;
            }
         }
      }

      protected void PA3D2( )
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
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridsignatures_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_62( ) ;
         while ( nGXsfl_6_idx <= nRC_GXsfl_6 )
         {
            sendrow_62( ) ;
            nGXsfl_6_idx = ((subGridsignatures_Islastpage==1)&&(nGXsfl_6_idx+1>subGridsignatures_fnc_Recordsperpage( )) ? 1 : nGXsfl_6_idx+1);
            sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
            SubsflControlProps_62( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridsignaturesContainer)) ;
         /* End function gxnrGridsignatures_newrow */
      }

      protected void gxgrGridsignatures_refresh( int subGridsignatures_Rows ,
                                                 Guid AV11groupId ,
                                                 GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow> AV22signatureRows ,
                                                 string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDSIGNATURES_nCurrentRecord = 0;
         RF3D2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridsignatures_refresh */
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
         RF3D2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlid_Enabled = 0;
         edtavCtldescription_Enabled = 0;
         edtavCtlsigneddatetime_Enabled = 0;
         edtavCtlsenderusername_Enabled = 0;
         chkavCtlcompleated.Enabled = 0;
         edtavCtlsendcoins_Enabled = 0;
         edtavCtlsendto_Enabled = 0;
         edtavSign_Enabled = 0;
      }

      protected void RF3D2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridsignaturesContainer.ClearRows();
         }
         wbStart = 6;
         /* Execute user event: Refresh */
         E123D2 ();
         nGXsfl_6_idx = 1;
         sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
         SubsflControlProps_62( ) ;
         bGXsfl_6_Refreshing = true;
         GridsignaturesContainer.AddObjectProperty("GridName", "Gridsignatures");
         GridsignaturesContainer.AddObjectProperty("CmpContext", sPrefix);
         GridsignaturesContainer.AddObjectProperty("InMasterPage", "false");
         GridsignaturesContainer.AddObjectProperty("Class", "Grid");
         GridsignaturesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridsignaturesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridsignaturesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridsignatures_Backcolorstyle), 1, 0, ".", "")));
         GridsignaturesContainer.PageSize = subGridsignatures_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_62( ) ;
            /* Execute user event: Gridsignatures.Load */
            E143D2 ();
            if ( ( subGridsignatures_Islastpage == 0 ) && ( GRIDSIGNATURES_nCurrentRecord > 0 ) && ( GRIDSIGNATURES_nGridOutOfScope == 0 ) && ( nGXsfl_6_idx == 1 ) )
            {
               GRIDSIGNATURES_nCurrentRecord = 0;
               GRIDSIGNATURES_nGridOutOfScope = 1;
               subgridsignatures_firstpage( ) ;
               /* Execute user event: Gridsignatures.Load */
               E143D2 ();
            }
            wbEnd = 6;
            WB3D0( ) ;
         }
         bGXsfl_6_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3D2( )
      {
      }

      protected int subGridsignatures_fnc_Pagecount( )
      {
         GRIDSIGNATURES_nRecordCount = subGridsignatures_fnc_Recordcount( );
         if ( ((long)((GRIDSIGNATURES_nRecordCount) % (subGridsignatures_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(GRIDSIGNATURES_nRecordCount/ (decimal)(subGridsignatures_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(GRIDSIGNATURES_nRecordCount/ (decimal)(subGridsignatures_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subGridsignatures_fnc_Recordcount( )
      {
         return AV22signatureRows.Count ;
      }

      protected int subGridsignatures_fnc_Recordsperpage( )
      {
         return (int)(20*1) ;
      }

      protected int subGridsignatures_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(GRIDSIGNATURES_nFirstRecordOnPage/ (decimal)(subGridsignatures_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subgridsignatures_firstpage( )
      {
         GRIDSIGNATURES_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDSIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDSIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridsignatures_refresh( subGridsignatures_Rows, AV11groupId, AV22signatureRows, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridsignatures_nextpage( )
      {
         GRIDSIGNATURES_nRecordCount = subGridsignatures_fnc_Recordcount( );
         if ( ( GRIDSIGNATURES_nRecordCount >= subGridsignatures_fnc_Recordsperpage( ) ) && ( GRIDSIGNATURES_nEOF == 0 ) )
         {
            GRIDSIGNATURES_nFirstRecordOnPage = (long)(GRIDSIGNATURES_nFirstRecordOnPage+subGridsignatures_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDSIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDSIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         GridsignaturesContainer.AddObjectProperty("GRIDSIGNATURES_nFirstRecordOnPage", GRIDSIGNATURES_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGridsignatures_refresh( subGridsignatures_Rows, AV11groupId, AV22signatureRows, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRIDSIGNATURES_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgridsignatures_previouspage( )
      {
         if ( GRIDSIGNATURES_nFirstRecordOnPage >= subGridsignatures_fnc_Recordsperpage( ) )
         {
            GRIDSIGNATURES_nFirstRecordOnPage = (long)(GRIDSIGNATURES_nFirstRecordOnPage-subGridsignatures_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDSIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDSIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridsignatures_refresh( subGridsignatures_Rows, AV11groupId, AV22signatureRows, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridsignatures_lastpage( )
      {
         GRIDSIGNATURES_nRecordCount = subGridsignatures_fnc_Recordcount( );
         if ( GRIDSIGNATURES_nRecordCount > subGridsignatures_fnc_Recordsperpage( ) )
         {
            if ( ((long)((GRIDSIGNATURES_nRecordCount) % (subGridsignatures_fnc_Recordsperpage( )))) == 0 )
            {
               GRIDSIGNATURES_nFirstRecordOnPage = (long)(GRIDSIGNATURES_nRecordCount-subGridsignatures_fnc_Recordsperpage( ));
            }
            else
            {
               GRIDSIGNATURES_nFirstRecordOnPage = (long)(GRIDSIGNATURES_nRecordCount-((long)((GRIDSIGNATURES_nRecordCount) % (subGridsignatures_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            GRIDSIGNATURES_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDSIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDSIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridsignatures_refresh( subGridsignatures_Rows, AV11groupId, AV22signatureRows, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgridsignatures_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            GRIDSIGNATURES_nFirstRecordOnPage = (long)(subGridsignatures_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            GRIDSIGNATURES_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDSIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDSIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridsignatures_refresh( subGridsignatures_Rows, AV11groupId, AV22signatureRows, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlid_Enabled = 0;
         edtavCtldescription_Enabled = 0;
         edtavCtlsigneddatetime_Enabled = 0;
         edtavCtlsenderusername_Enabled = 0;
         chkavCtlcompleated.Enabled = 0;
         edtavCtlsendcoins_Enabled = 0;
         edtavCtlsendto_Enabled = 0;
         edtavSign_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3D0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Signaturerows"), AV22signatureRows);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vSIGNATUREROWS"), AV22signatureRows);
            /* Read saved values. */
            nRC_GXsfl_6 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_6"), ".", ","), 18, MidpointRounding.ToEven));
            wcpOAV11groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV11groupId"));
            GRIDSIGNATURES_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDSIGNATURES_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            GRIDSIGNATURES_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDSIGNATURES_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_6 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_6"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_6_fel_idx = 0;
            while ( nGXsfl_6_fel_idx < nRC_GXsfl_6 )
            {
               nGXsfl_6_fel_idx = ((subGridsignatures_Islastpage==1)&&(nGXsfl_6_fel_idx+1>subGridsignatures_fnc_Recordsperpage( )) ? 1 : nGXsfl_6_fel_idx+1);
               sGXsfl_6_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_62( ) ;
               AV24GXV1 = (int)(nGXsfl_6_fel_idx+GRIDSIGNATURES_nFirstRecordOnPage);
               if ( ( AV22signatureRows.Count >= AV24GXV1 ) && ( AV24GXV1 > 0 ) )
               {
                  AV22signatureRows.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1));
                  AV18sign = cgiGet( edtavSign_Internalname);
               }
            }
            if ( nGXsfl_6_fel_idx == 0 )
            {
               nGXsfl_6_idx = 1;
               sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
               SubsflControlProps_62( ) ;
            }
            nGXsfl_6_fel_idx = 1;
            /* Read variables values. */
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            /* Check if conditions changed and reset current page numbers */
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void E123D2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         AV19websession.Set("MuSign_ONE", "");
         GXt_objcol_SdtLegacySignatureRow1 = AV22signatureRows;
         new GeneXus.Programs.wallet.registered.getlegacysignaturerows(context ).execute(  AV11groupId, out  GXt_objcol_SdtLegacySignatureRow1) ;
         AV22signatureRows = GXt_objcol_SdtLegacySignatureRow1;
         gx_BV6 = true;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV22signatureRows", AV22signatureRows);
      }

      protected void E133D2( )
      {
         AV24GXV1 = (int)(nGXsfl_6_idx+GRIDSIGNATURES_nFirstRecordOnPage);
         if ( ( AV24GXV1 > 0 ) && ( AV22signatureRows.Count >= AV24GXV1 ) )
         {
            AV22signatureRows.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1));
         }
         /* 'Sign Transaction' Routine */
         returnInSub = false;
         GXt_char2 = AV21error;
         new GeneXus.Programs.wallet.registered.setlegacysignaturetosign(context ).execute(  AV11groupId,  ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)(AV22signatureRows.CurrentItem)).gxTpr_Rowindex,  ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)(AV22signatureRows.CurrentItem)).gxTpr_Id, out  AV23groupType, out  GXt_char2) ;
         AV21error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV21error)) )
         {
            if ( AV23groupType == 30 )
            {
               context.PopUp(formatLink("wallet.registered.sendcoinsdelegated") , new Object[] {});
            }
            else
            {
               context.PopUp(formatLink("wallet.registered.sendcoinslegacy") , new Object[] {});
            }
         }
         else
         {
            GX_msglist.addItem(AV21error);
         }
      }

      private void E143D2( )
      {
         /* Gridsignatures_Load Routine */
         returnInSub = false;
         AV24GXV1 = 1;
         while ( AV24GXV1 <= AV22signatureRows.Count )
         {
            AV22signatureRows.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1));
            if ( ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)(AV22signatureRows.CurrentItem)).gxTpr_Cansign )
            {
               AV18sign = "Sign transaction";
               AssignAttri(sPrefix, false, edtavSign_Internalname, AV18sign);
            }
            else
            {
               AV18sign = "";
               AssignAttri(sPrefix, false, edtavSign_Internalname, AV18sign);
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 6;
            }
            if ( ( subGridsignatures_Islastpage == 1 ) || ( 20 == 0 ) || ( ( GRIDSIGNATURES_nCurrentRecord >= GRIDSIGNATURES_nFirstRecordOnPage ) && ( GRIDSIGNATURES_nCurrentRecord < GRIDSIGNATURES_nFirstRecordOnPage + subGridsignatures_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_62( ) ;
            }
            GRIDSIGNATURES_nEOF = (short)(((subGridsignatures_Rows==0)||(GRIDSIGNATURES_nCurrentRecord<GRIDSIGNATURES_nFirstRecordOnPage+subGridsignatures_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRIDSIGNATURES_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDSIGNATURES_nEOF), 1, 0, ".", "")));
            GRIDSIGNATURES_nCurrentRecord = (long)(GRIDSIGNATURES_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_6_Refreshing )
            {
               DoAjaxLoad(6, GridsignaturesRow);
            }
            AV24GXV1 = (int)(AV24GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E113D2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV17sendCoinPopupName = "Wallet.registered.SendCoinsLegacy";
         AV5strFound = (short)(StringUtil.StringSearch( AV15PopupName, StringUtil.Lower( AV17sendCoinPopupName), 1));
         if ( AV5strFound <= 0 )
         {
            AV17sendCoinPopupName = "Wallet.registered.SendCoinsDelegated";
            AV5strFound = (short)(StringUtil.StringSearch( AV15PopupName, StringUtil.Lower( AV17sendCoinPopupName), 1));
         }
         if ( AV5strFound > 0 )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "RefreshSmartGroup", new Object[] {}, true);
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV11groupId = (Guid)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV11groupId", AV11groupId.ToString());
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
         PA3D2( ) ;
         WS3D2( ) ;
         WE3D2( ) ;
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
         sCtrlAV11groupId = (string)((string)getParm(obj,0));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA3D2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\legacyallsignatures", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA3D2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV11groupId = (Guid)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV11groupId", AV11groupId.ToString());
         }
         wcpOAV11groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV11groupId"));
         if ( ! GetJustCreated( ) && ( ( AV11groupId != wcpOAV11groupId ) ) )
         {
            setjustcreated();
         }
         wcpOAV11groupId = AV11groupId;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV11groupId = cgiGet( sPrefix+"AV11groupId_CTRL");
         if ( StringUtil.Len( sCtrlAV11groupId) > 0 )
         {
            AV11groupId = StringUtil.StrToGuid( cgiGet( sCtrlAV11groupId));
            AssignAttri(sPrefix, false, "AV11groupId", AV11groupId.ToString());
         }
         else
         {
            AV11groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"AV11groupId_PARM"));
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
         PA3D2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS3D2( ) ;
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
         WS3D2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV11groupId_PARM", AV11groupId.ToString());
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV11groupId)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV11groupId_CTRL", StringUtil.RTrim( sCtrlAV11groupId));
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
         WE3D2( ) ;
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
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610817252730", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/legacyallsignatures.js", "?202610817252730", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_62( )
      {
         edtavCtlid_Internalname = sPrefix+"CTLID_"+sGXsfl_6_idx;
         edtavCtldescription_Internalname = sPrefix+"CTLDESCRIPTION_"+sGXsfl_6_idx;
         edtavCtlsigneddatetime_Internalname = sPrefix+"CTLSIGNEDDATETIME_"+sGXsfl_6_idx;
         edtavCtlsenderusername_Internalname = sPrefix+"CTLSENDERUSERNAME_"+sGXsfl_6_idx;
         chkavCtlcompleated_Internalname = sPrefix+"CTLCOMPLEATED_"+sGXsfl_6_idx;
         edtavCtlsendcoins_Internalname = sPrefix+"CTLSENDCOINS_"+sGXsfl_6_idx;
         edtavCtlsendto_Internalname = sPrefix+"CTLSENDTO_"+sGXsfl_6_idx;
         edtavSign_Internalname = sPrefix+"vSIGN_"+sGXsfl_6_idx;
      }

      protected void SubsflControlProps_fel_62( )
      {
         edtavCtlid_Internalname = sPrefix+"CTLID_"+sGXsfl_6_fel_idx;
         edtavCtldescription_Internalname = sPrefix+"CTLDESCRIPTION_"+sGXsfl_6_fel_idx;
         edtavCtlsigneddatetime_Internalname = sPrefix+"CTLSIGNEDDATETIME_"+sGXsfl_6_fel_idx;
         edtavCtlsenderusername_Internalname = sPrefix+"CTLSENDERUSERNAME_"+sGXsfl_6_fel_idx;
         chkavCtlcompleated_Internalname = sPrefix+"CTLCOMPLEATED_"+sGXsfl_6_fel_idx;
         edtavCtlsendcoins_Internalname = sPrefix+"CTLSENDCOINS_"+sGXsfl_6_fel_idx;
         edtavCtlsendto_Internalname = sPrefix+"CTLSENDTO_"+sGXsfl_6_fel_idx;
         edtavSign_Internalname = sPrefix+"vSIGN_"+sGXsfl_6_fel_idx;
      }

      protected void sendrow_62( )
      {
         sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
         SubsflControlProps_62( ) ;
         WB3D0( ) ;
         if ( ( 20 * 1 == 0 ) || ( nGXsfl_6_idx <= subGridsignatures_fnc_Recordsperpage( ) * 1 ) )
         {
            GridsignaturesRow = GXWebRow.GetNew(context,GridsignaturesContainer);
            if ( subGridsignatures_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subGridsignatures_Backstyle = 0;
               if ( StringUtil.StrCmp(subGridsignatures_Class, "") != 0 )
               {
                  subGridsignatures_Linesclass = subGridsignatures_Class+"Odd";
               }
            }
            else if ( subGridsignatures_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subGridsignatures_Backstyle = 0;
               subGridsignatures_Backcolor = subGridsignatures_Allbackcolor;
               if ( StringUtil.StrCmp(subGridsignatures_Class, "") != 0 )
               {
                  subGridsignatures_Linesclass = subGridsignatures_Class+"Uniform";
               }
            }
            else if ( subGridsignatures_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subGridsignatures_Backstyle = 1;
               if ( StringUtil.StrCmp(subGridsignatures_Class, "") != 0 )
               {
                  subGridsignatures_Linesclass = subGridsignatures_Class+"Odd";
               }
               subGridsignatures_Backcolor = (int)(0x0);
            }
            else if ( subGridsignatures_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subGridsignatures_Backstyle = 1;
               if ( ((int)((nGXsfl_6_idx) % (2))) == 0 )
               {
                  subGridsignatures_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridsignatures_Class, "") != 0 )
                  {
                     subGridsignatures_Linesclass = subGridsignatures_Class+"Even";
                  }
               }
               else
               {
                  subGridsignatures_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridsignatures_Class, "") != 0 )
                  {
                     subGridsignatures_Linesclass = subGridsignatures_Class+"Odd";
                  }
               }
            }
            if ( GridsignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_6_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridsignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridsignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlid_Internalname,((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Id.ToString(),((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Id.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)6,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridsignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
            ROClassString = "Attribute";
            GridsignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtldescription_Internalname,((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Description,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtldescription_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtldescription_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridsignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 9,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
            ROClassString = "Attribute";
            GridsignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlsigneddatetime_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Signeddatetime, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Signeddatetime, "99/99/99 99:99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',8,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',8,12,'eng',false,0);"+";gx.evt.onblur(this,9);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlsigneddatetime_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlsigneddatetime_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridsignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
            ROClassString = "Attribute";
            GridsignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlsenderusername_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Senderusername),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,10);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlsenderusername_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlsenderusername_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridsignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
            }
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
            ClassString = "Attribute";
            StyleString = "";
            GXCCtl = "CTLCOMPLEATED_" + sGXsfl_6_idx;
            chkavCtlcompleated.Name = GXCCtl;
            chkavCtlcompleated.WebTags = "";
            chkavCtlcompleated.Caption = "";
            AssignProp(sPrefix, false, chkavCtlcompleated_Internalname, "TitleCaption", chkavCtlcompleated.Caption, !bGXsfl_6_Refreshing);
            chkavCtlcompleated.CheckedValue = "false";
            GridsignaturesRow.AddColumnProperties("checkbox", 1, isAjaxCallMode( ), new Object[] {(string)chkavCtlcompleated_Internalname,StringUtil.BoolToStr( ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Compleated),(string)"",(string)"",(short)-1,chkavCtlcompleated.Enabled,(string)"true",(string)"",(string)StyleString,(string)ClassString,(string)"",(string)"",TempTags+" onclick="+"\"gx.fn.checkboxClick(11, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,11);\""});
            /* Subfile cell */
            if ( GridsignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
            ROClassString = "Attribute";
            GridsignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlsendcoins_Internalname,StringUtil.LTrim( StringUtil.NToC( ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Sendcoins, 16, 8, ".", "")),StringUtil.LTrim( ((edtavCtlsendcoins_Enabled!=0) ? context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Sendcoins, "ZZZZZZ9.99999999") : context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Sendcoins, "ZZZZZZ9.99999999"))),TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,12);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlsendcoins_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlsendcoins_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)16,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridsignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
            ROClassString = "Attribute";
            GridsignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlsendto_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtLegacySignatureRow)AV22signatureRows.Item(AV24GXV1)).gxTpr_Sendto),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlsendto_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlsendto_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridsignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
            ROClassString = "Attribute";
            GridsignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavSign_Internalname,StringUtil.RTrim( AV18sign),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,14);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SIGN TRANSACTION\\'."+sGXsfl_6_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavSign_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavSign_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes3D2( ) ;
            GridsignaturesContainer.AddRow(GridsignaturesRow);
            nGXsfl_6_idx = ((subGridsignatures_Islastpage==1)&&(nGXsfl_6_idx+1>subGridsignatures_fnc_Recordsperpage( )) ? 1 : nGXsfl_6_idx+1);
            sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
            SubsflControlProps_62( ) ;
         }
         /* End function sendrow_62 */
      }

      protected void init_web_controls( )
      {
         GXCCtl = "CTLCOMPLEATED_" + sGXsfl_6_idx;
         chkavCtlcompleated.Name = GXCCtl;
         chkavCtlcompleated.WebTags = "";
         chkavCtlcompleated.Caption = "";
         AssignProp(sPrefix, false, chkavCtlcompleated_Internalname, "TitleCaption", chkavCtlcompleated.Caption, !bGXsfl_6_Refreshing);
         chkavCtlcompleated.CheckedValue = "false";
         /* End function init_web_controls */
      }

      protected void StartGridControl6( )
      {
         if ( GridsignaturesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridsignaturesContainer"+"DivS\" data-gxgridid=\"6\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridsignatures_Internalname, subGridsignatures_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridsignatures_Backcolorstyle == 0 )
            {
               subGridsignatures_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridsignatures_Class) > 0 )
               {
                  subGridsignatures_Linesclass = subGridsignatures_Class+"Title";
               }
            }
            else
            {
               subGridsignatures_Titlebackstyle = 1;
               if ( subGridsignatures_Backcolorstyle == 1 )
               {
                  subGridsignatures_Titlebackcolor = subGridsignatures_Allbackcolor;
                  if ( StringUtil.Len( subGridsignatures_Class) > 0 )
                  {
                     subGridsignatures_Linesclass = subGridsignatures_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridsignatures_Class) > 0 )
                  {
                     subGridsignatures_Linesclass = subGridsignatures_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "id") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Description") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Signed Date Time") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Signed By") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Fully signed") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Amount (coins)") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Send to") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridsignaturesContainer.AddObjectProperty("GridName", "Gridsignatures");
         }
         else
         {
            GridsignaturesContainer.AddObjectProperty("GridName", "Gridsignatures");
            GridsignaturesContainer.AddObjectProperty("Header", subGridsignatures_Header);
            GridsignaturesContainer.AddObjectProperty("Class", "Grid");
            GridsignaturesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridsignaturesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridsignaturesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridsignatures_Backcolorstyle), 1, 0, ".", "")));
            GridsignaturesContainer.AddObjectProperty("CmpContext", sPrefix);
            GridsignaturesContainer.AddObjectProperty("InMasterPage", "false");
            GridsignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridsignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlid_Enabled), 5, 0, ".", "")));
            GridsignaturesContainer.AddColumnProperties(GridsignaturesColumn);
            GridsignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridsignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtldescription_Enabled), 5, 0, ".", "")));
            GridsignaturesContainer.AddColumnProperties(GridsignaturesColumn);
            GridsignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridsignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlsigneddatetime_Enabled), 5, 0, ".", "")));
            GridsignaturesContainer.AddColumnProperties(GridsignaturesColumn);
            GridsignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridsignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlsenderusername_Enabled), 5, 0, ".", "")));
            GridsignaturesContainer.AddColumnProperties(GridsignaturesColumn);
            GridsignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridsignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkavCtlcompleated.Enabled), 5, 0, ".", "")));
            GridsignaturesContainer.AddColumnProperties(GridsignaturesColumn);
            GridsignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridsignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlsendcoins_Enabled), 5, 0, ".", "")));
            GridsignaturesContainer.AddColumnProperties(GridsignaturesColumn);
            GridsignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridsignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlsendto_Enabled), 5, 0, ".", "")));
            GridsignaturesContainer.AddColumnProperties(GridsignaturesColumn);
            GridsignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridsignaturesColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV18sign)));
            GridsignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavSign_Enabled), 5, 0, ".", "")));
            GridsignaturesContainer.AddColumnProperties(GridsignaturesColumn);
            GridsignaturesContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridsignatures_Selectedindex), 4, 0, ".", "")));
            GridsignaturesContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridsignatures_Allowselection), 1, 0, ".", "")));
            GridsignaturesContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridsignatures_Selectioncolor), 9, 0, ".", "")));
            GridsignaturesContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridsignatures_Allowhovering), 1, 0, ".", "")));
            GridsignaturesContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridsignatures_Hoveringcolor), 9, 0, ".", "")));
            GridsignaturesContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridsignatures_Allowcollapsing), 1, 0, ".", "")));
            GridsignaturesContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridsignatures_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavCtlid_Internalname = sPrefix+"CTLID";
         edtavCtldescription_Internalname = sPrefix+"CTLDESCRIPTION";
         edtavCtlsigneddatetime_Internalname = sPrefix+"CTLSIGNEDDATETIME";
         edtavCtlsenderusername_Internalname = sPrefix+"CTLSENDERUSERNAME";
         chkavCtlcompleated_Internalname = sPrefix+"CTLCOMPLEATED";
         edtavCtlsendcoins_Internalname = sPrefix+"CTLSENDCOINS";
         edtavCtlsendto_Internalname = sPrefix+"CTLSENDTO";
         edtavSign_Internalname = sPrefix+"vSIGN";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGridsignatures_Internalname = sPrefix+"GRIDSIGNATURES";
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
         subGridsignatures_Allowcollapsing = 0;
         subGridsignatures_Allowselection = 0;
         subGridsignatures_Header = "";
         edtavSign_Jsonclick = "";
         edtavSign_Enabled = 1;
         edtavCtlsendto_Jsonclick = "";
         edtavCtlsendto_Enabled = 0;
         edtavCtlsendcoins_Jsonclick = "";
         edtavCtlsendcoins_Enabled = 0;
         chkavCtlcompleated.Caption = "";
         chkavCtlcompleated.Enabled = 0;
         edtavCtlsenderusername_Jsonclick = "";
         edtavCtlsenderusername_Enabled = 0;
         edtavCtlsigneddatetime_Jsonclick = "";
         edtavCtlsigneddatetime_Enabled = 0;
         edtavCtldescription_Jsonclick = "";
         edtavCtldescription_Enabled = 0;
         edtavCtlid_Jsonclick = "";
         edtavCtlid_Enabled = 0;
         subGridsignatures_Class = "Grid";
         subGridsignatures_Backcolorstyle = 0;
         edtavCtlsendto_Enabled = -1;
         edtavCtlsendcoins_Enabled = -1;
         chkavCtlcompleated.Enabled = -1;
         edtavCtlsenderusername_Enabled = -1;
         edtavCtlsigneddatetime_Enabled = -1;
         edtavCtldescription_Enabled = -1;
         edtavCtlid_Enabled = -1;
         subGridsignatures_Rows = 20;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDSIGNATURES_nEOF","type":"int"},{"av":"subGridsignatures_Rows","ctrl":"GRIDSIGNATURES","prop":"Rows"},{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV11groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"}]}""");
         setEventMetadata("'SIGN TRANSACTION'","""{"handler":"E133D2","iparms":[{"av":"AV11groupId","fld":"vGROUPID","type":"guid"},{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"}]}""");
         setEventMetadata("GRIDSIGNATURES.LOAD","""{"handler":"E143D2","iparms":[{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"}]""");
         setEventMetadata("GRIDSIGNATURES.LOAD",""","oparms":[{"av":"AV18sign","fld":"vSIGN","type":"char"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E113D2","iparms":[{"av":"AV15PopupName","fld":"vPOPUPNAME","type":"char"}]}""");
         setEventMetadata("GRIDSIGNATURES_FIRSTPAGE","""{"handler":"subgridsignatures_firstpage","iparms":[{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDSIGNATURES_nEOF","type":"int"},{"av":"subGridsignatures_Rows","ctrl":"GRIDSIGNATURES","prop":"Rows"},{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV11groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("GRIDSIGNATURES_FIRSTPAGE",""","oparms":[{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"}]}""");
         setEventMetadata("GRIDSIGNATURES_PREVPAGE","""{"handler":"subgridsignatures_previouspage","iparms":[{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDSIGNATURES_nEOF","type":"int"},{"av":"subGridsignatures_Rows","ctrl":"GRIDSIGNATURES","prop":"Rows"},{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV11groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("GRIDSIGNATURES_PREVPAGE",""","oparms":[{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"}]}""");
         setEventMetadata("GRIDSIGNATURES_NEXTPAGE","""{"handler":"subgridsignatures_nextpage","iparms":[{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDSIGNATURES_nEOF","type":"int"},{"av":"subGridsignatures_Rows","ctrl":"GRIDSIGNATURES","prop":"Rows"},{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV11groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("GRIDSIGNATURES_NEXTPAGE",""","oparms":[{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"}]}""");
         setEventMetadata("GRIDSIGNATURES_LASTPAGE","""{"handler":"subgridsignatures_lastpage","iparms":[{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDSIGNATURES_nEOF","type":"int"},{"av":"subGridsignatures_Rows","ctrl":"GRIDSIGNATURES","prop":"Rows"},{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV11groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("GRIDSIGNATURES_LASTPAGE",""","oparms":[{"av":"AV22signatureRows","fld":"vSIGNATUREROWS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDSIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDSIGNATURES","prop":"GridRC","grid":6,"type":"int"}]}""");
         setEventMetadata("VALIDV_GXV2","""{"handler":"Validv_Gxv2","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Sign","iparms":[]}""");
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
         wcpOAV11groupId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV22signatureRows = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow>( context, "LegacySignatureRow", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV15PopupName = "";
         GX_FocusControl = "";
         GridsignaturesContainer = new GXWebGrid( context);
         sStyleString = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV18sign = "";
         AV19websession = context.GetSession();
         GXt_objcol_SdtLegacySignatureRow1 = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow>( context, "LegacySignatureRow", "distributedcryptography");
         AV21error = "";
         GXt_char2 = "";
         GridsignaturesRow = new GXWebRow();
         AV17sendCoinPopupName = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV11groupId = "";
         subGridsignatures_Linesclass = "";
         ROClassString = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         GXCCtl = "";
         GridsignaturesColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlid_Enabled = 0;
         edtavCtldescription_Enabled = 0;
         edtavCtlsigneddatetime_Enabled = 0;
         edtavCtlsenderusername_Enabled = 0;
         chkavCtlcompleated.Enabled = 0;
         edtavCtlsendcoins_Enabled = 0;
         edtavCtlsendto_Enabled = 0;
         edtavSign_Enabled = 0;
      }

      private short GRIDSIGNATURES_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridsignatures_Backcolorstyle ;
      private short AV23groupType ;
      private short AV5strFound ;
      private short nGXWrapped ;
      private short subGridsignatures_Backstyle ;
      private short subGridsignatures_Titlebackstyle ;
      private short subGridsignatures_Allowselection ;
      private short subGridsignatures_Allowhovering ;
      private short subGridsignatures_Allowcollapsing ;
      private short subGridsignatures_Collapsed ;
      private int nRC_GXsfl_6 ;
      private int subGridsignatures_Rows ;
      private int nGXsfl_6_idx=1 ;
      private int edtavCtlid_Enabled ;
      private int edtavCtldescription_Enabled ;
      private int edtavCtlsigneddatetime_Enabled ;
      private int edtavCtlsenderusername_Enabled ;
      private int edtavCtlsendcoins_Enabled ;
      private int edtavCtlsendto_Enabled ;
      private int edtavSign_Enabled ;
      private int AV24GXV1 ;
      private int subGridsignatures_Islastpage ;
      private int GRIDSIGNATURES_nGridOutOfScope ;
      private int nGXsfl_6_fel_idx=1 ;
      private int idxLst ;
      private int subGridsignatures_Backcolor ;
      private int subGridsignatures_Allbackcolor ;
      private int subGridsignatures_Titlebackcolor ;
      private int subGridsignatures_Selectedindex ;
      private int subGridsignatures_Selectioncolor ;
      private int subGridsignatures_Hoveringcolor ;
      private long GRIDSIGNATURES_nFirstRecordOnPage ;
      private long GRIDSIGNATURES_nCurrentRecord ;
      private long GRIDSIGNATURES_nRecordCount ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_6_idx="0001" ;
      private string edtavCtlid_Internalname ;
      private string edtavCtldescription_Internalname ;
      private string edtavCtlsigneddatetime_Internalname ;
      private string edtavCtlsenderusername_Internalname ;
      private string chkavCtlcompleated_Internalname ;
      private string edtavCtlsendcoins_Internalname ;
      private string edtavCtlsendto_Internalname ;
      private string edtavSign_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV15PopupName ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string sStyleString ;
      private string subGridsignatures_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV18sign ;
      private string sGXsfl_6_fel_idx="0001" ;
      private string AV21error ;
      private string GXt_char2 ;
      private string AV17sendCoinPopupName ;
      private string sCtrlAV11groupId ;
      private string subGridsignatures_Class ;
      private string subGridsignatures_Linesclass ;
      private string ROClassString ;
      private string edtavCtlid_Jsonclick ;
      private string TempTags ;
      private string edtavCtldescription_Jsonclick ;
      private string edtavCtlsigneddatetime_Jsonclick ;
      private string edtavCtlsenderusername_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string GXCCtl ;
      private string edtavCtlsendcoins_Jsonclick ;
      private string edtavCtlsendto_Jsonclick ;
      private string edtavSign_Jsonclick ;
      private string subGridsignatures_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_6_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool gx_BV6 ;
      private Guid AV11groupId ;
      private Guid wcpOAV11groupId ;
      private GXWebGrid GridsignaturesContainer ;
      private GXWebRow GridsignaturesRow ;
      private GXWebColumn GridsignaturesColumn ;
      private GXWebForm Form ;
      private IGxSession AV19websession ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkavCtlcompleated ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow> AV22signatureRows ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtLegacySignatureRow> GXt_objcol_SdtLegacySignatureRow1 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
