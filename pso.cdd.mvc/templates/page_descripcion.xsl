<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="1.1"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:ms="urn:schemas-microsoft-com:xslt"
	xmlns:fo="http://www.w3.org/1999/XSL/Format"
	exclude-result-prefixes="fo">
  <xsl:include href="page_style.xsl"/>
  <xsl:template name="page-descripcion"  match="ComprobanteDescanso">
    <xsl:param name = "nomCopia" />
    <fo:table>
      <fo:table-column column-width="19.5cm"/>
      <fo:table-body> 
        <fo:table-row>
          <fo:table-cell xsl:use-attribute-sets="cajaDato-Cell">
            <fo:block space-before="0.1cm" >
              <fo:table  xsl:use-attribute-sets="master-Table">
                <fo:table-column column-width="0.5cm"/>
                <fo:table-column column-width="1.3cm"/>
                <fo:table-column column-width="1.3cm"/>
                <fo:table-column column-width="1.3cm"/>
                <fo:table-column column-width="2.1cm"/>
                <fo:table-column column-width="0.5cm"/>
                <fo:table-column column-width="5.5cm"/>
                <fo:table-column column-width="0.5cm"/>
                <fo:table-column column-width="2.1cm"/>
                <fo:table-column column-width="1.3cm"/>
                <fo:table-column column-width="1.3cm"/>
                <fo:table-column column-width="1.3cm"/>
                <fo:table-body line-height="15pt"> 
                  <!-- FILA 2 -->
                  <fo:table-row>
                    <fo:table-cell number-columns-spanned="3" number-rows-spanned="2">
                      <fo:block>
                        <fo:external-graphic src="url('Images/logo.jpg')"  height="0.5cm"/>
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell number-columns-spanned="5" xsl:use-attribute-sets="titulo-Box">
                      <fo:block>COMPROBANTE DE DESCANSO DIA DOMINGO</fo:block>                    
                    </fo:table-cell>
                    <fo:table-cell>
                      <fo:block>&#x00A0;</fo:block>                    
                    </fo:table-cell>  
                    <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block>DIA</fo:block>
                    </fo:table-cell>
                  <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block>MES</fo:block>
                    </fo:table-cell>
                  <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block>AÑO</fo:block>
                    </fo:table-cell> 
                  </fo:table-row>
                  <!-- FILA 3 -->
                  <fo:table-row> 
                    <fo:table-cell number-columns-spanned="5" xsl:use-attribute-sets="centrado negrita">
                      <fo:block>CONTROL LEY 20.823</fo:block>                    
                    </fo:table-cell>
                    <fo:table-cell>
                      <fo:block>&#x00A0;</fo:block>                    
                    </fo:table-cell>  
                    <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block><xsl:value-of select="FechaDia" /></fo:block>
                    </fo:table-cell>
                  <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block><xsl:value-of select="FechaMes" /></fo:block>
                    </fo:table-cell>
                  <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block><xsl:value-of select="FechaAno" /></fo:block>
                    </fo:table-cell> 
                  </fo:table-row>
                  <!-- FILA 4 -->
                  <fo:table-row>
                    <fo:table-cell number-columns-spanned="12">
                      <fo:block>&#x00A0;</fo:block>
                    </fo:table-cell>                  
                  </fo:table-row>
                  <!-- FILA 5 -->
                  <fo:table-row>
                    <fo:table-cell></fo:table-cell>
                    <fo:table-cell number-columns-spanned="6" xsl:use-attribute-sets="label-caja">
                      <fo:block>NOMBRE DEL TRABAJADOR</fo:block>                    
                    </fo:table-cell>                  
                  <fo:table-cell>
                    </fo:table-cell> 
                  <fo:table-cell number-columns-spanned="4" xsl:use-attribute-sets="label-caja">
                      <fo:block>EDP</fo:block>                    
                    </fo:table-cell> 
                  </fo:table-row>
                  <!-- FILA 6 -->
                  <fo:table-row>
                    <fo:table-cell></fo:table-cell>
                    <fo:table-cell number-columns-spanned="6" xsl:use-attribute-sets="generalBorde">
                      <fo:block><xsl:value-of select="TrabajadorNombre" /></fo:block>
                    </fo:table-cell>
                    <fo:table-cell>
                    </fo:table-cell>
                    <fo:table-cell number-columns-spanned="4" xsl:use-attribute-sets="generalBorde">
                      <fo:block><xsl:value-of select="EDP" /></fo:block>
                    </fo:table-cell>
                  </fo:table-row> 
                  <!-- FILA 8 -->
                  <fo:table-row>
                    <fo:table-cell></fo:table-cell>
                    <fo:table-cell number-columns-spanned="4" xsl:use-attribute-sets="label-caja">
                      <fo:block>RUT</fo:block>
                    </fo:table-cell>
                    <fo:table-cell>
                    </fo:table-cell>
                    <fo:table-cell number-columns-spanned="5" xsl:use-attribute-sets="label-caja">
                      <fo:block>CARGO</fo:block>
                    </fo:table-cell>
                  </fo:table-row>
                  <!-- FILA 9 -->
                  <fo:table-row>
                    <fo:table-cell></fo:table-cell>
                    <fo:table-cell number-columns-spanned="4" xsl:use-attribute-sets="generalBorde">
                      <fo:block>
                        <xsl:value-of select="TrabajadorRut" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell>
                    </fo:table-cell>
                    <fo:table-cell number-columns-spanned="5" xsl:use-attribute-sets="generalBorde">
                      <fo:block>
                        <xsl:value-of select="TrabajadorCargo" />
                      </fo:block>
                    </fo:table-cell>
                  </fo:table-row> 
                  <!-- FILA 11 -->
                  <fo:table-row>
                    <fo:table-cell></fo:table-cell>
                    <fo:table-cell number-columns-spanned="11" xsl:use-attribute-sets="label-caja">
                      <fo:block>FECHA DIA DESCANSO A OTORGAR</fo:block>
                    </fo:table-cell>
                  </fo:table-row>
                  <!-- FILA 12 -->
                  <fo:table-row>
                    <fo:table-cell></fo:table-cell>
                    <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block>DIA</fo:block>
                    </fo:table-cell>
                    <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block>MES</fo:block>
                    </fo:table-cell>
                    <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block>AÑO</fo:block>
                    </fo:table-cell>
                  </fo:table-row>
                  <!-- FILA 13 -->
                  <fo:table-row>
                    <fo:table-cell></fo:table-cell>
                    <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block>
                        <xsl:value-of select="DescansoDia" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block>
                        <xsl:value-of select="DescansoMes" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell xsl:use-attribute-sets="generalBorde centrado">
                      <fo:block>
                        <xsl:value-of select="DescansoAno" />
                      </fo:block>
                    </fo:table-cell>
                  </fo:table-row>
                  <!-- FILA 14 -->
                  <fo:table-row>
                    <fo:table-cell number-columns-spanned="12" xsl:use-attribute-sets="doble-alto">
                      <fo:block>&#x00A0;</fo:block>
                    </fo:table-cell>
                  </fo:table-row> 
                  <!-- FILA 15 -->
                  <fo:table-row>
                    <fo:table-cell>
                      <fo:block>&#x00A0;</fo:block>
                    </fo:table-cell>
                    <fo:table-cell number-columns-spanned="4" xsl:use-attribute-sets="borde-abajo">
                      <fo:block>TRABAJADOR</fo:block>
                    </fo:table-cell>
                    <fo:table-cell>
                      <fo:block>&#x00A0;</fo:block>
                    </fo:table-cell>
                    <fo:table-cell xsl:use-attribute-sets="borde-abajo">
                      <fo:block>JEFE DE TIENDA </fo:block>
                    </fo:table-cell>
                    <fo:table-cell>
                      <fo:block>&#x00A0;</fo:block>
                    </fo:table-cell>
                    <fo:table-cell number-columns-spanned="4" xsl:use-attribute-sets="borde-abajo">
                      <fo:block>RECURSOS HUMANOS</fo:block>
                    </fo:table-cell>
                  </fo:table-row>
                  <!-- FILA 16 -->
                  <fo:table-row>
                    <fo:table-cell number-columns-spanned="12" xsl:use-attribute-sets="copy-Box">
                      <fo:block>COPIA&#x00A0;<xsl:value-of select = "$nomCopia" /></fo:block>
                    </fo:table-cell>
                  </fo:table-row>
                </fo:table-body>
              </fo:table>
            </fo:block>
          </fo:table-cell>
        </fo:table-row>
      </fo:table-body>

    </fo:table>
  </xsl:template>
</xsl:stylesheet>