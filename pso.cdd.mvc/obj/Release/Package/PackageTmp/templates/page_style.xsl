<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="1.1"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:ms="urn:schemas-microsoft-com:xslt"
	xmlns:fo="http://www.w3.org/1999/XSL/Format"
	exclude-result-prefixes="fo">
  <!-- ATRIBUTOS BASE -->
  <xsl:attribute-set name="generalBorde">
    <xsl:attribute name="border">solid 0.1mm black</xsl:attribute>
    <xsl:attribute name="margin-right">1mm</xsl:attribute>
    <xsl:attribute name="margin-left">1mm</xsl:attribute>
    <xsl:attribute name="margin-top">1mm</xsl:attribute> 
    <xsl:attribute name="padding">0pt</xsl:attribute>
  </xsl:attribute-set>

  <!-- ATRIBUTO PARA FORMATO DOCUMENTO -->
  <xsl:attribute-set name="body-Format">
    <xsl:attribute name="font-family">Calibri</xsl:attribute>
    <xsl:attribute name="font-size">8pt</xsl:attribute>
  </xsl:attribute-set>

  <!-- ATRIBUTO PARA CAJA DE DATO DINAMICO -->
  <xsl:attribute-set name="cajaDato-Cell" use-attribute-sets="generalBorde">
    <xsl:attribute name="padding">1pt 0pt 1pt 2pt</xsl:attribute>
    <xsl:attribute name="overflow">hidden</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="cajaDato-Box">
    <xsl:attribute name="padding-left">0.5mm</xsl:attribute>
    <xsl:attribute name="overflow">hidden</xsl:attribute>
  </xsl:attribute-set>
  <!-- ATRIBUTO PARA ETIQUETA DE DATOS -->
  <xsl:attribute-set name="cajaLabel-Cell">
  </xsl:attribute-set>
  <xsl:attribute-set name="cajaLabel-Box">
  </xsl:attribute-set>
  <!-- ATRIBUTO PARA CABECERA DE TABLA -->
  <xsl:attribute-set name="header-Table" use-attribute-sets="generalBorde">
    <xsl:attribute name="background-color">#F2F2F7</xsl:attribute>
    <xsl:attribute name="font-weight">bold</xsl:attribute>
    <xsl:attribute name="margin-left">1mm</xsl:attribute>
  </xsl:attribute-set>

  <!-- ATRIBUTO PARA BOX TITULO PAGINA -->
  <xsl:attribute-set name="titulo-Cell" use-attribute-sets="generalBorde">
    <xsl:attribute name="background-color">#F2F2F2</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="titulo-Box">
    <xsl:attribute name="text-align">center</xsl:attribute>
    <xsl:attribute name="font-weight">bold</xsl:attribute>
    <xsl:attribute name="font-size">1.5em</xsl:attribute>
  </xsl:attribute-set>

  <xsl:attribute-set name="master-Table">
    <xsl:attribute name="border-collapse">separate</xsl:attribute>
    <xsl:attribute name="border-separation">0pt</xsl:attribute> 
  </xsl:attribute-set> 
  <xsl:preserve-space elements="*"/>

  <!-- ALINEACION -->
  <xsl:attribute-set name="numero">
    <xsl:attribute name="text-align">right</xsl:attribute>
  </xsl:attribute-set>

  <xsl:attribute-set name="fecha">
    <xsl:attribute name="text-align">right</xsl:attribute>
  </xsl:attribute-set>
  
  <xsl:attribute-set name="centrado">
    <xsl:attribute name="text-align">center</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="negrita">
    <xsl:attribute name="font-weight">bold</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="borde-abajo">
    <xsl:attribute name="border-top">solid 0.1mm black</xsl:attribute>
    <xsl:attribute name="text-align">center</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="doble-alto"> 
    <xsl:attribute name="height">1.4cm</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="copy-Box">
    <xsl:attribute name="text-align">right</xsl:attribute> 
    <xsl:attribute name="font-size">0.7em</xsl:attribute> 
  </xsl:attribute-set>
  <xsl:attribute-set name="label-caja">
    <xsl:attribute name="padding-top">0.1cm</xsl:attribute>
  </xsl:attribute-set>
  <xsl:decimal-format name="numero" decimal-separator="," grouping-separator="." NaN="0"/>
</xsl:stylesheet>

