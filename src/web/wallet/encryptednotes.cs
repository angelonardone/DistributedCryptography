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
   public class encryptednotes : GXDataArea
   {
      public encryptednotes( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public encryptednotes( IGxContext context )
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridnotes") == 0 )
            {
               gxnrGridnotes_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridnotes") == 0 )
            {
               gxgrGridnotes_refresh_invoke( ) ;
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

      protected void gxnrGridnotes_newrow_invoke( )
      {
         nRC_GXsfl_9 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_9"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_9_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_9_idx = GetPar( "sGXsfl_9_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridnotes_newrow( ) ;
         /* End function gxnrGridnotes_newrow_invoke */
      }

      protected void gxgrGridnotes_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV12wallet);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridnotes_refresh( AV12wallet) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridnotes_refresh_invoke */
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
         PA102( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START102( ) ;
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
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 123260), false, true);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.encryptednotes") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV12wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV12wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV12wallet, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Notesread", AV8notesRead);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Notesread", AV8notesRead);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_9", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_9), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vNOTESREAD", AV8notesRead);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vNOTESREAD", AV8notesRead);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vUSERRESPONSE", AV9UserResponse);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV12wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV12wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV12wallet, context));
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
         if ( ! ( WebComp_Compnewnote == null ) )
         {
            WebComp_Compnewnote.componentjscripts();
         }
      }

      public override void RenderHtmlContent( )
      {
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE102( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT102( ) ;
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
         return formatLink("wallet.encryptednotes")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.EncryptedNotes" ;
      }

      public override string GetPgmdesc( )
      {
         return "Encrypted Notes" ;
      }

      protected void WB100( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 6,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCreateanewnote_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(9), 1, 0)+","+"null"+");", "Create a new Note", bttCreateanewnote_Jsonclick, 5, "Create a new Note", "", StyleString, ClassString, bttCreateanewnote_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CREATE A NEW NOTE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/EncryptedNotes.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridnotesContainer.SetWrapped(nGXWrapped);
            StartGridControl9( ) ;
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            nRC_GXsfl_9 = (int)(nGXsfl_9_idx-1);
            if ( GridnotesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV13GXV1 = nGXsfl_9_idx;
               if ( subGridnotes_Visible != 0 )
               {
                  sStyleString = "";
               }
               else
               {
                  sStyleString = " style=\"display:none;\"";
               }
               context.WriteHtmlText( "<div id=\""+"GridnotesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridnotes", GridnotesContainer, subGridnotes_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridnotesContainerData", GridnotesContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridnotesContainerData"+"V", GridnotesContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridnotesContainerData"+"V"+"\" value='"+GridnotesContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, "W0015"+"", StringUtil.RTrim( WebComp_Compnewnote_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+"gxHTMLWrpW0015"+""+"\""+((WebComp_Compnewnote_Visible==1) ? "" : " style=\"display:none;\"")) ;
               context.WriteHtmlText( ">") ;
               if ( bGXsfl_9_Refreshing )
               {
                  if ( StringUtil.Len( WebComp_Compnewnote_Component) != 0 )
                  {
                     if ( StringUtil.StrCmp(StringUtil.Lower( OldCompnewnote), StringUtil.Lower( WebComp_Compnewnote_Component)) != 0 )
                     {
                        context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0015"+"");
                     }
                     WebComp_Compnewnote.componentdraw();
                     if ( StringUtil.StrCmp(StringUtil.Lower( OldCompnewnote), StringUtil.Lower( WebComp_Compnewnote_Component)) != 0 )
                     {
                        context.httpAjaxContext.ajax_rspEndCmp();
                     }
                  }
               }
               context.WriteHtmlText( "</div>") ;
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridnotesContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV13GXV1 = nGXsfl_9_idx;
                  if ( subGridnotes_Visible != 0 )
                  {
                     sStyleString = "";
                  }
                  else
                  {
                     sStyleString = " style=\"display:none;\"";
                  }
                  context.WriteHtmlText( "<div id=\""+"GridnotesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridnotes", GridnotesContainer, subGridnotes_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridnotesContainerData", GridnotesContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridnotesContainerData"+"V", GridnotesContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridnotesContainerData"+"V"+"\" value='"+GridnotesContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START102( )
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
         Form.Meta.addItem("description", "Encrypted Notes", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP100( ) ;
      }

      protected void WS102( )
      {
         START102( ) ;
         EVT102( ) ;
      }

      protected void EVT102( )
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
                           else if ( StringUtil.StrCmp(sEvt, "'CREATE A NEW NOTE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Create a new Note' */
                              E11102 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GLOBALEVENTS.DONEWITHNOTES") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E12102 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E13102 ();
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 11), "'OPEN NOTE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'DELETE NOTE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "GRIDNOTES.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 11), "'OPEN NOTE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'DELETE NOTE'") == 0 ) )
                           {
                              nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
                              SubsflControlProps_92( ) ;
                              AV13GXV1 = nGXsfl_9_idx;
                              if ( ( AV8notesRead.Count >= AV13GXV1 ) && ( AV13GXV1 > 0 ) )
                              {
                                 AV8notesRead.CurrentItem = ((GeneXus.Programs.wallet.SdtNoteRead)AV8notesRead.Item(AV13GXV1));
                                 AV10delete = cgiGet( edtavDelete_Internalname);
                                 AssignAttri("", false, edtavDelete_Internalname, AV10delete);
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
                                    E14102 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E15102 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'OPEN NOTE'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Open Note' */
                                    E16102 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DELETE NOTE'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Delete Note' */
                                    E17102 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDNOTES.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridnotes.Load */
                                    E18102 ();
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
                     else if ( StringUtil.StrCmp(sEvtType, "W") == 0 )
                     {
                        sEvtType = StringUtil.Left( sEvt, 4);
                        sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-4));
                        nCmpId = (short)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                        if ( nCmpId == 15 )
                        {
                           OldCompnewnote = cgiGet( "W0015");
                           if ( ( StringUtil.Len( OldCompnewnote) == 0 ) || ( StringUtil.StrCmp(OldCompnewnote, WebComp_Compnewnote_Component) != 0 ) )
                           {
                              WebComp_Compnewnote = getWebComponent(GetType(), "GeneXus.Programs", OldCompnewnote, new Object[] {context} );
                              WebComp_Compnewnote.ComponentInit();
                              WebComp_Compnewnote.Name = "OldCompnewnote";
                              WebComp_Compnewnote_Component = OldCompnewnote;
                           }
                           if ( StringUtil.Len( WebComp_Compnewnote_Component) != 0 )
                           {
                              WebComp_Compnewnote.componentprocess("W0015", "", sEvt);
                           }
                           WebComp_Compnewnote_Component = OldCompnewnote;
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE102( )
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

      protected void PA102( )
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
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridnotes_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_92( ) ;
         while ( nGXsfl_9_idx <= nRC_GXsfl_9 )
         {
            sendrow_92( ) ;
            nGXsfl_9_idx = ((subGridnotes_Islastpage==1)&&(nGXsfl_9_idx+1>subGridnotes_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridnotesContainer)) ;
         /* End function gxnrGridnotes_newrow */
      }

      protected void gxgrGridnotes_refresh( GeneXus.Programs.wallet.SdtWallet AV12wallet )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDNOTES_nCurrentRecord = 0;
         RF102( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridnotes_refresh */
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
         RF102( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtldescription1_Enabled = 0;
         edtavCtlcreated1_Enabled = 0;
         edtavDelete_Enabled = 0;
      }

      protected void RF102( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridnotesContainer.ClearRows();
         }
         wbStart = 9;
         /* Execute user event: Refresh */
         E15102 ();
         nGXsfl_9_idx = 1;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         bGXsfl_9_Refreshing = true;
         GridnotesContainer.AddObjectProperty("GridName", "Gridnotes");
         GridnotesContainer.AddObjectProperty("CmpContext", "");
         GridnotesContainer.AddObjectProperty("InMasterPage", "false");
         GridnotesContainer.AddObjectProperty("Class", "Grid");
         GridnotesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridnotesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridnotesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Backcolorstyle), 1, 0, ".", "")));
         GridnotesContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Visible), 5, 0, ".", "")));
         GridnotesContainer.PageSize = subGridnotes_fnc_Recordsperpage( );
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            if ( WebComp_Compnewnote_Visible != 0 )
            {
               if ( StringUtil.Len( WebComp_Compnewnote_Component) != 0 )
               {
                  WebComp_Compnewnote.componentstart();
               }
            }
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_92( ) ;
            /* Execute user event: Gridnotes.Load */
            E18102 ();
            wbEnd = 9;
            WB100( ) ;
         }
         bGXsfl_9_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes102( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV12wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV12wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV12wallet, context));
      }

      protected int subGridnotes_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridnotes_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridnotes_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridnotes_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtldescription1_Enabled = 0;
         edtavCtlcreated1_Enabled = 0;
         edtavDelete_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP100( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E14102 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Notesread"), AV8notesRead);
            ajax_req_read_hidden_sdt(cgiGet( "vNOTESREAD"), AV8notesRead);
            /* Read saved values. */
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_9_fel_idx = 0;
            while ( nGXsfl_9_fel_idx < nRC_GXsfl_9 )
            {
               nGXsfl_9_fel_idx = ((subGridnotes_Islastpage==1)&&(nGXsfl_9_fel_idx+1>subGridnotes_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_fel_idx+1);
               sGXsfl_9_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_92( ) ;
               AV13GXV1 = nGXsfl_9_fel_idx;
               if ( ( AV8notesRead.Count >= AV13GXV1 ) && ( AV13GXV1 > 0 ) )
               {
                  AV8notesRead.CurrentItem = ((GeneXus.Programs.wallet.SdtNoteRead)AV8notesRead.Item(AV13GXV1));
                  AV10delete = cgiGet( edtavDelete_Internalname);
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
         E14102 ();
         if (returnInSub) return;
      }

      protected void E14102( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_SdtWallet1 = AV12wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV12wallet = GXt_SdtWallet1;
      }

      protected void E15102( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         AV10delete = "Delete";
         AssignAttri("", false, edtavDelete_Internalname, AV10delete);
         GXt_objcol_SdtNoteRead2 = AV8notesRead;
         new GeneXus.Programs.wallet.readallnotes(context ).execute( out  GXt_objcol_SdtNoteRead2) ;
         AV8notesRead = GXt_objcol_SdtNoteRead2;
         gx_BV9 = true;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV8notesRead", AV8notesRead);
      }

      protected void E11102( )
      {
         /* 'Create a new Note' Routine */
         returnInSub = false;
         subGridnotes_Visible = 0;
         AssignProp("", false, "GridnotesContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subGridnotes_Visible), 5, 0), true);
         bttCreateanewnote_Visible = 0;
         AssignProp("", false, bttCreateanewnote_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreateanewnote_Visible), 5, 0), true);
         WebComp_Compnewnote_Visible = 1;
         AssignProp("", false, "gxHTMLWrpW0015"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Compnewnote_Visible), 5, 0), true);
         /* Object Property */
         if ( true )
         {
            bDynCreated_Compnewnote = true;
         }
         if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Compnewnote_Component), StringUtil.Lower( "Wallet.EditNote")) != 0 )
         {
            WebComp_Compnewnote = getWebComponent(GetType(), "GeneXus.Programs", "wallet.editnote", new Object[] {context} );
            WebComp_Compnewnote.ComponentInit();
            WebComp_Compnewnote.Name = "Wallet.EditNote";
            WebComp_Compnewnote_Component = "Wallet.EditNote";
         }
         if ( StringUtil.Len( WebComp_Compnewnote_Component) != 0 )
         {
            WebComp_Compnewnote.setjustcreated();
            WebComp_Compnewnote.componentprepare(new Object[] {(string)"W0015",(string)"",(string)""});
            WebComp_Compnewnote.componentbind(new Object[] {(string)""});
         }
         if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Compnewnote )
         {
            context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0015"+"");
            WebComp_Compnewnote.componentdraw();
            context.httpAjaxContext.ajax_rspEndCmp();
         }
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
         if ( gx_BV9 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV8notesRead", AV8notesRead);
            nGXsfl_9_bak_idx = nGXsfl_9_idx;
            gxgrGridnotes_refresh( AV12wallet) ;
            nGXsfl_9_idx = nGXsfl_9_bak_idx;
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
      }

      protected void E16102( )
      {
         AV13GXV1 = nGXsfl_9_idx;
         if ( ( AV13GXV1 > 0 ) && ( AV8notesRead.Count >= AV13GXV1 ) )
         {
            AV8notesRead.CurrentItem = ((GeneXus.Programs.wallet.SdtNoteRead)AV8notesRead.Item(AV13GXV1));
         }
         /* 'Open Note' Routine */
         returnInSub = false;
         subGridnotes_Visible = 0;
         AssignProp("", false, "GridnotesContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subGridnotes_Visible), 5, 0), true);
         bttCreateanewnote_Visible = 0;
         AssignProp("", false, bttCreateanewnote_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreateanewnote_Visible), 5, 0), true);
         WebComp_Compnewnote_Visible = 1;
         AssignProp("", false, "gxHTMLWrpW0015"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Compnewnote_Visible), 5, 0), true);
         /* Object Property */
         if ( true )
         {
            bDynCreated_Compnewnote = true;
         }
         if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Compnewnote_Component), StringUtil.Lower( "Wallet.EditNote")) != 0 )
         {
            WebComp_Compnewnote = getWebComponent(GetType(), "GeneXus.Programs", "wallet.editnote", new Object[] {context} );
            WebComp_Compnewnote.ComponentInit();
            WebComp_Compnewnote.Name = "Wallet.EditNote";
            WebComp_Compnewnote_Component = "Wallet.EditNote";
         }
         if ( StringUtil.Len( WebComp_Compnewnote_Component) != 0 )
         {
            WebComp_Compnewnote.setjustcreated();
            WebComp_Compnewnote.componentprepare(new Object[] {(string)"W0015",(string)"",((GeneXus.Programs.wallet.SdtNoteRead)(AV8notesRead.CurrentItem)).gxTpr_Notefilename});
            WebComp_Compnewnote.componentbind(new Object[] {(string)""});
         }
         if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Compnewnote )
         {
            context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0015"+"");
            WebComp_Compnewnote.componentdraw();
            context.httpAjaxContext.ajax_rspEndCmp();
         }
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV8notesRead", AV8notesRead);
         nGXsfl_9_bak_idx = nGXsfl_9_idx;
         gxgrGridnotes_refresh( AV12wallet) ;
         nGXsfl_9_idx = nGXsfl_9_bak_idx;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
      }

      protected void E12102( )
      {
         AV13GXV1 = nGXsfl_9_idx;
         if ( ( AV13GXV1 > 0 ) && ( AV8notesRead.Count >= AV13GXV1 ) )
         {
            AV8notesRead.CurrentItem = ((GeneXus.Programs.wallet.SdtNoteRead)AV8notesRead.Item(AV13GXV1));
         }
         /* GlobalEvents_Donewithnotes Routine */
         returnInSub = false;
         subGridnotes_Visible = 1;
         AssignProp("", false, "GridnotesContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subGridnotes_Visible), 5, 0), true);
         bttCreateanewnote_Visible = 1;
         AssignProp("", false, bttCreateanewnote_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreateanewnote_Visible), 5, 0), true);
         WebComp_Compnewnote_Visible = 0;
         AssignProp("", false, "gxHTMLWrpW0015"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Compnewnote_Visible), 5, 0), true);
         GXt_objcol_SdtNoteRead2 = AV8notesRead;
         new GeneXus.Programs.wallet.readallnotes(context ).execute( out  GXt_objcol_SdtNoteRead2) ;
         AV8notesRead = GXt_objcol_SdtNoteRead2;
         gx_BV9 = true;
         gxgrGridnotes_refresh( AV12wallet) ;
         /*  Sending Event outputs  */
         if ( gx_BV9 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV8notesRead", AV8notesRead);
            nGXsfl_9_bak_idx = nGXsfl_9_idx;
            gxgrGridnotes_refresh( AV12wallet) ;
            nGXsfl_9_idx = nGXsfl_9_bak_idx;
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
      }

      protected void E17102( )
      {
         AV13GXV1 = nGXsfl_9_idx;
         if ( ( AV13GXV1 > 0 ) && ( AV8notesRead.Count >= AV13GXV1 ) )
         {
            AV8notesRead.CurrentItem = ((GeneXus.Programs.wallet.SdtNoteRead)AV8notesRead.Item(AV13GXV1));
         }
         /* 'Delete Note' Routine */
         returnInSub = false;
         this.executeExternalObjectMethod("", false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to Delete "+((GeneXus.Programs.wallet.SdtNoteRead)(AV8notesRead.CurrentItem)).gxTpr_Description+" Note?"}, false);
      }

      protected void E13102( )
      {
         AV13GXV1 = nGXsfl_9_idx;
         if ( ( AV13GXV1 > 0 ) && ( AV8notesRead.Count >= AV13GXV1 ) )
         {
            AV8notesRead.CurrentItem = ((GeneXus.Programs.wallet.SdtNoteRead)AV8notesRead.Item(AV13GXV1));
         }
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV9UserResponse )
         {
            GXt_boolean3 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
            GXt_boolean4 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
            AV11file.Source = AV12wallet.gxTpr_Walletbasedirectory+(GXt_boolean4 ? "/" : "\\")+StringUtil.Trim( ((GeneXus.Programs.wallet.SdtNoteRead)(AV8notesRead.CurrentItem)).gxTpr_Notefilename);
            if ( AV11file.Exists() )
            {
               AV11file.Delete();
               GXt_objcol_SdtNoteRead2 = AV8notesRead;
               new GeneXus.Programs.wallet.readallnotes(context ).execute( out  GXt_objcol_SdtNoteRead2) ;
               AV8notesRead = GXt_objcol_SdtNoteRead2;
               gx_BV9 = true;
            }
            else
            {
               GX_msglist.addItem("The file does not exist");
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV8notesRead", AV8notesRead);
         nGXsfl_9_bak_idx = nGXsfl_9_idx;
         gxgrGridnotes_refresh( AV12wallet) ;
         nGXsfl_9_idx = nGXsfl_9_bak_idx;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
      }

      private void E18102( )
      {
         /* Gridnotes_Load Routine */
         returnInSub = false;
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV8notesRead.Count )
         {
            AV8notesRead.CurrentItem = ((GeneXus.Programs.wallet.SdtNoteRead)AV8notesRead.Item(AV13GXV1));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 9;
            }
            sendrow_92( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_9_Refreshing )
            {
               DoAjaxLoad(9, GridnotesRow);
            }
            AV13GXV1 = (int)(AV13GXV1+1);
         }
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
         PA102( ) ;
         WS102( ) ;
         WE102( ) ;
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
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         if ( ! ( WebComp_Compnewnote == null ) )
         {
            if ( StringUtil.Len( WebComp_Compnewnote_Component) != 0 )
            {
               WebComp_Compnewnote.componentthemes();
            }
         }
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20255213173839", true, true);
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
         context.AddJavascriptSource("wallet/encryptednotes.js", "?20255213173839", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_92( )
      {
         edtavCtldescription1_Internalname = "CTLDESCRIPTION1_"+sGXsfl_9_idx;
         edtavCtlcreated1_Internalname = "CTLCREATED1_"+sGXsfl_9_idx;
         edtavDelete_Internalname = "vDELETE_"+sGXsfl_9_idx;
      }

      protected void SubsflControlProps_fel_92( )
      {
         edtavCtldescription1_Internalname = "CTLDESCRIPTION1_"+sGXsfl_9_fel_idx;
         edtavCtlcreated1_Internalname = "CTLCREATED1_"+sGXsfl_9_fel_idx;
         edtavDelete_Internalname = "vDELETE_"+sGXsfl_9_fel_idx;
      }

      protected void sendrow_92( )
      {
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         WB100( ) ;
         GridnotesRow = GXWebRow.GetNew(context,GridnotesContainer);
         if ( subGridnotes_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridnotes_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridnotes_Class, "") != 0 )
            {
               subGridnotes_Linesclass = subGridnotes_Class+"Odd";
            }
         }
         else if ( subGridnotes_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridnotes_Backstyle = 0;
            subGridnotes_Backcolor = subGridnotes_Allbackcolor;
            if ( StringUtil.StrCmp(subGridnotes_Class, "") != 0 )
            {
               subGridnotes_Linesclass = subGridnotes_Class+"Uniform";
            }
         }
         else if ( subGridnotes_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridnotes_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridnotes_Class, "") != 0 )
            {
               subGridnotes_Linesclass = subGridnotes_Class+"Odd";
            }
            subGridnotes_Backcolor = (int)(0x0);
         }
         else if ( subGridnotes_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridnotes_Backstyle = 1;
            if ( ((int)((nGXsfl_9_idx) % (2))) == 0 )
            {
               subGridnotes_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridnotes_Class, "") != 0 )
               {
                  subGridnotes_Linesclass = subGridnotes_Class+"Even";
               }
            }
            else
            {
               subGridnotes_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridnotes_Class, "") != 0 )
               {
                  subGridnotes_Linesclass = subGridnotes_Class+"Odd";
               }
            }
         }
         if ( GridnotesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_9_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridnotesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridnotesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtldescription1_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtNoteRead)AV8notesRead.Item(AV13GXV1)).gxTpr_Description),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,10);\"","'"+""+"'"+",false,"+"'"+"E\\'OPEN NOTE\\'."+sGXsfl_9_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtldescription1_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtldescription1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)80,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridnotesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridnotesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcreated1_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtNoteRead)AV8notesRead.Item(AV13GXV1)).gxTpr_Created, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtNoteRead)AV8notesRead.Item(AV13GXV1)).gxTpr_Created, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,11);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcreated1_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcreated1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridnotesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridnotesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDelete_Internalname,StringUtil.RTrim( AV10delete),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,12);\"","'"+""+"'"+",false,"+"'"+"E\\'DELETE NOTE\\'."+sGXsfl_9_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDelete_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavDelete_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         send_integrity_lvl_hashes102( ) ;
         GridnotesContainer.AddRow(GridnotesRow);
         nGXsfl_9_idx = ((subGridnotes_Islastpage==1)&&(nGXsfl_9_idx+1>subGridnotes_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
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
         if ( GridnotesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridnotesContainer"+"DivS\" data-gxgridid=\"9\">") ;
            sStyleString = "";
            if ( subGridnotes_Visible == 0 )
            {
               sStyleString += "display:none;";
            }
            GxWebStd.gx_table_start( context, subGridnotes_Internalname, subGridnotes_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridnotes_Backcolorstyle == 0 )
            {
               subGridnotes_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridnotes_Class) > 0 )
               {
                  subGridnotes_Linesclass = subGridnotes_Class+"Title";
               }
            }
            else
            {
               subGridnotes_Titlebackstyle = 1;
               if ( subGridnotes_Backcolorstyle == 1 )
               {
                  subGridnotes_Titlebackcolor = subGridnotes_Allbackcolor;
                  if ( StringUtil.Len( subGridnotes_Class) > 0 )
                  {
                     subGridnotes_Linesclass = subGridnotes_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridnotes_Class) > 0 )
                  {
                     subGridnotes_Linesclass = subGridnotes_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Description") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Created") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridnotesContainer.AddObjectProperty("GridName", "Gridnotes");
         }
         else
         {
            GridnotesContainer.AddObjectProperty("GridName", "Gridnotes");
            GridnotesContainer.AddObjectProperty("Header", subGridnotes_Header);
            GridnotesContainer.AddObjectProperty("Class", "Grid");
            GridnotesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridnotesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridnotesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Backcolorstyle), 1, 0, ".", "")));
            GridnotesContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Visible), 5, 0, ".", "")));
            GridnotesContainer.AddObjectProperty("CmpContext", "");
            GridnotesContainer.AddObjectProperty("InMasterPage", "false");
            GridnotesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridnotesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtldescription1_Enabled), 5, 0, ".", "")));
            GridnotesContainer.AddColumnProperties(GridnotesColumn);
            GridnotesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridnotesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcreated1_Enabled), 5, 0, ".", "")));
            GridnotesContainer.AddColumnProperties(GridnotesColumn);
            GridnotesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridnotesColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV10delete)));
            GridnotesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDelete_Enabled), 5, 0, ".", "")));
            GridnotesContainer.AddColumnProperties(GridnotesColumn);
            GridnotesContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Selectedindex), 4, 0, ".", "")));
            GridnotesContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Allowselection), 1, 0, ".", "")));
            GridnotesContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Selectioncolor), 9, 0, ".", "")));
            GridnotesContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Allowhovering), 1, 0, ".", "")));
            GridnotesContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Hoveringcolor), 9, 0, ".", "")));
            GridnotesContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Allowcollapsing), 1, 0, ".", "")));
            GridnotesContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnotes_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         bttCreateanewnote_Internalname = "CREATEANEWNOTE";
         edtavCtldescription1_Internalname = "CTLDESCRIPTION1";
         edtavCtlcreated1_Internalname = "CTLCREATED1";
         edtavDelete_Internalname = "vDELETE";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subGridnotes_Internalname = "GRIDNOTES";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridnotes_Allowcollapsing = 0;
         subGridnotes_Allowselection = 0;
         subGridnotes_Header = "";
         edtavDelete_Jsonclick = "";
         edtavDelete_Enabled = 1;
         edtavCtlcreated1_Jsonclick = "";
         edtavCtlcreated1_Enabled = 0;
         edtavCtldescription1_Jsonclick = "";
         edtavCtldescription1_Enabled = 0;
         subGridnotes_Class = "Grid";
         subGridnotes_Backcolorstyle = 0;
         edtavCtlcreated1_Enabled = -1;
         edtavCtldescription1_Enabled = -1;
         WebComp_Compnewnote_Visible = 1;
         AssignProp("", false, "gxHTMLWrpW0015"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Compnewnote_Visible), 5, 0), true);
         subGridnotes_Visible = 1;
         bttCreateanewnote_Visible = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Encrypted Notes";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"GRIDNOTES_nEOF","type":"int"},{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV12wallet","fld":"vWALLET","hsh":true,"type":""}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV10delete","fld":"vDELETE","type":"char"},{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("'CREATE A NEW NOTE'","""{"handler":"E11102","iparms":[{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"GRIDNOTES_nEOF","type":"int"},{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV12wallet","fld":"vWALLET","hsh":true,"type":""}]""");
         setEventMetadata("'CREATE A NEW NOTE'",""","oparms":[{"av":"subGridnotes_Visible","ctrl":"GRIDNOTES","prop":"Visible"},{"ctrl":"CREATEANEWNOTE","prop":"Visible"},{"ctrl":"COMPNEWNOTE","prop":"Visible"},{"ctrl":"COMPNEWNOTE"},{"av":"AV10delete","fld":"vDELETE","type":"char"},{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("'OPEN NOTE'","""{"handler":"E16102","iparms":[{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"GRIDNOTES_nEOF","type":"int"},{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV12wallet","fld":"vWALLET","hsh":true,"type":""}]""");
         setEventMetadata("'OPEN NOTE'",""","oparms":[{"av":"subGridnotes_Visible","ctrl":"GRIDNOTES","prop":"Visible"},{"ctrl":"CREATEANEWNOTE","prop":"Visible"},{"ctrl":"COMPNEWNOTE","prop":"Visible"},{"ctrl":"COMPNEWNOTE"},{"av":"AV10delete","fld":"vDELETE","type":"char"},{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("GLOBALEVENTS.DONEWITHNOTES","""{"handler":"E12102","iparms":[{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"GRIDNOTES_nEOF","type":"int"},{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV12wallet","fld":"vWALLET","hsh":true,"type":""}]""");
         setEventMetadata("GLOBALEVENTS.DONEWITHNOTES",""","oparms":[{"av":"subGridnotes_Visible","ctrl":"GRIDNOTES","prop":"Visible"},{"ctrl":"CREATEANEWNOTE","prop":"Visible"},{"ctrl":"COMPNEWNOTE","prop":"Visible"},{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV10delete","fld":"vDELETE","type":"char"}]}""");
         setEventMetadata("'DELETE NOTE'","""{"handler":"E17102","iparms":[{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E13102","iparms":[{"av":"AV9UserResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV12wallet","fld":"vWALLET","hsh":true,"type":""},{"av":"GRIDNOTES_nEOF","type":"int"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV8notesRead","fld":"vNOTESREAD","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDNOTES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDNOTES","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Delete","iparms":[]}""");
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
         AV12wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV8notesRead = new GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead>( context, "NoteRead", "distributedcryptography");
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttCreateanewnote_Jsonclick = "";
         GridnotesContainer = new GXWebGrid( context);
         sStyleString = "";
         WebComp_Compnewnote_Component = "";
         OldCompnewnote = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV10delete = "";
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV11file = new GxFile(context.GetPhysicalPath());
         GXt_objcol_SdtNoteRead2 = new GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead>( context, "NoteRead", "distributedcryptography");
         GridnotesRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridnotes_Linesclass = "";
         ROClassString = "";
         GridnotesColumn = new GXWebColumn();
         WebComp_Compnewnote = new GeneXus.Http.GXNullWebComponent();
         /* GeneXus formulas. */
         edtavCtldescription1_Enabled = 0;
         edtavCtlcreated1_Enabled = 0;
         edtavDelete_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nCmpId ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridnotes_Backcolorstyle ;
      private short GRIDNOTES_nEOF ;
      private short nGXWrapped ;
      private short subGridnotes_Backstyle ;
      private short subGridnotes_Titlebackstyle ;
      private short subGridnotes_Allowselection ;
      private short subGridnotes_Allowhovering ;
      private short subGridnotes_Allowcollapsing ;
      private short subGridnotes_Collapsed ;
      private int nRC_GXsfl_9 ;
      private int nGXsfl_9_idx=1 ;
      private int bttCreateanewnote_Visible ;
      private int AV13GXV1 ;
      private int subGridnotes_Visible ;
      private int WebComp_Compnewnote_Visible ;
      private int subGridnotes_Islastpage ;
      private int edtavCtldescription1_Enabled ;
      private int edtavCtlcreated1_Enabled ;
      private int edtavDelete_Enabled ;
      private int nGXsfl_9_fel_idx=1 ;
      private int nGXsfl_9_bak_idx=1 ;
      private int idxLst ;
      private int subGridnotes_Backcolor ;
      private int subGridnotes_Allbackcolor ;
      private int subGridnotes_Titlebackcolor ;
      private int subGridnotes_Selectedindex ;
      private int subGridnotes_Selectioncolor ;
      private int subGridnotes_Hoveringcolor ;
      private long GRIDNOTES_nCurrentRecord ;
      private long GRIDNOTES_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_9_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttCreateanewnote_Internalname ;
      private string bttCreateanewnote_Jsonclick ;
      private string sStyleString ;
      private string subGridnotes_Internalname ;
      private string WebComp_Compnewnote_Component ;
      private string OldCompnewnote ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV10delete ;
      private string edtavDelete_Internalname ;
      private string sGXsfl_9_fel_idx="0001" ;
      private string edtavCtldescription1_Internalname ;
      private string edtavCtlcreated1_Internalname ;
      private string subGridnotes_Class ;
      private string subGridnotes_Linesclass ;
      private string ROClassString ;
      private string edtavCtldescription1_Jsonclick ;
      private string edtavCtlcreated1_Jsonclick ;
      private string edtavDelete_Jsonclick ;
      private string subGridnotes_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV9UserResponse ;
      private bool wbLoad ;
      private bool bGXsfl_9_Refreshing=false ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool gx_BV9 ;
      private bool bDynCreated_Compnewnote ;
      private bool GXt_boolean3 ;
      private bool GXt_boolean4 ;
      private GXWebComponent WebComp_Compnewnote ;
      private GXWebGrid GridnotesContainer ;
      private GXWebRow GridnotesRow ;
      private GXWebColumn GridnotesColumn ;
      private GxFile AV11file ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.SdtWallet AV12wallet ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead> AV8notesRead ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtNoteRead> GXt_objcol_SdtNoteRead2 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
