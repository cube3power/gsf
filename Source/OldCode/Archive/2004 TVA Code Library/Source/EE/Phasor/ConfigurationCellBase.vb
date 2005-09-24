'*******************************************************************************************************
'  ConfigurationCellBase.vb - Configuration cell base class
'  Copyright � 2005 - TVA, all rights reserved - Gbtc
'
'  Build Environment: VB.NET, Visual Studio 2003
'  Primary Developer: James R Carroll, System Analyst [TVA]
'      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
'       Phone: 423/751-2827
'       Email: jrcarrol@tva.gov
'
'  Code Modification History:
'  -----------------------------------------------------------------------------------------------------
'  01/14/2005 - James R Carroll
'       Initial version of source generated
'
'*******************************************************************************************************

Imports System.Buffer
Imports System.Text
Imports TVA.Interop
Imports TVA.EE.Phasor.Common

Namespace EE.Phasor

    ' This class represents the protocol independent common implementation of a set of configuration related data settings that can be sent or received from a PMU.
    Public MustInherit Class ConfigurationCellBase

        Inherits ChannelCellBase
        Implements IConfigurationCell

        Private m_stationName As String
        Private m_idCode As Int16
        Private m_idLabel As String
        Private m_phasorDefinitions As PhasorDefinitionCollection
        Private m_frequencyDefinition As IFrequencyDefinition
        Private m_analogDefinitions As AnalogDefinitionCollection
        Private m_digitalDefinitions As DigitalDefinitionCollection
        Private m_sampleRate As Int16

        Protected Sub New(ByVal parent As IConfigurationFrame, ByVal alignOnDWordBoundry As Boolean, ByVal maximumPhasors As Integer, ByVal maximumAnalogs As Integer, ByVal maximumDigitals As Integer)

            MyBase.New(parent, alignOnDWordBoundry)

            m_phasorDefinitions = New PhasorDefinitionCollection(maximumPhasors)
            m_analogDefinitions = New AnalogDefinitionCollection(maximumAnalogs)
            m_digitalDefinitions = New DigitalDefinitionCollection(maximumDigitals)

        End Sub

        Protected Sub New(ByVal parent As IDataFrame, ByVal alignOnDWordBoundry As Boolean, ByVal maximumPhasors As Integer, ByVal maximumAnalogs As Integer, ByVal maximumDigitals As Integer, ByVal state As IConfigurationCellParsingState, ByVal binaryImage As Byte(), ByVal startIndex As Integer)

            Me.New(parent, alignOnDWordBoundry, maximumPhasors, maximumAnalogs, maximumDigitals)
            ParseBinaryImage(state, binaryImage, startIndex)

        End Sub

        Protected Sub New(ByVal parent As IConfigurationFrame, ByVal alignOnDWordBoundry As Boolean, ByVal stationName As String, ByVal idCode As Int16, ByVal idLabel As String, ByVal phasorDefinitions As PhasorDefinitionCollection, ByVal frequencyDefinition As IFrequencyDefinition, ByVal analogDefinitions As AnalogDefinitionCollection, ByVal digitalDefinitions As DigitalDefinitionCollection, ByVal sampleRate As Int16)

            MyBase.New(parent, alignOnDWordBoundry)

            Me.StationName = stationName
            m_idCode = idCode
            Me.IDLabel = idLabel
            m_phasorDefinitions = phasorDefinitions
            m_frequencyDefinition = frequencyDefinition
            m_analogDefinitions = analogDefinitions
            m_digitalDefinitions = digitalDefinitions
            m_sampleRate = sampleRate

        End Sub

        ' Final dervived classes must expose Public Sub New(ByVal parent As IChannelFrame, ByVal state As IChannelFrameParsingState, ByVal index As Integer, ByVal binaryImage As Byte(), ByVal startIndex As Integer)

        ' Derived classes are expected to expose a Public Sub New(ByVal configurationCell As IConfigurationCell)
        Protected Sub New(ByVal configurationCell As IConfigurationCell)

            Me.New(configurationCell.Parent, configurationCell.AlignOnDWordBoundry, configurationCell.StationName, configurationCell.IDCode, _
                configurationCell.IDLabel, configurationCell.PhasorDefinitions, configurationCell.FrequencyDefinition, _
                configurationCell.AnalogDefinitions, configurationCell.DigitalDefinitions, configurationCell.SampleRate)

        End Sub

        Public Overridable Shadows ReadOnly Property Parent() As IConfigurationFrame Implements IConfigurationCell.Parent
            Get
                Return MyBase.Parent
            End Get
        End Property

        Public Overridable Property StationName() As String Implements IConfigurationCell.StationName
            Get
                Return m_stationName
            End Get
            Set(ByVal Value As String)
                If Len(Trim(Value)) > MaximumStationNameLength Then
                    Throw New OverflowException("Station name length cannot exceed " & MaximumStationNameLength)
                Else
                    m_stationName = Trim(Replace(Value, Chr(20), " "))
                End If
            End Set
        End Property

        Public Overridable ReadOnly Property StationNameImage() As Byte() Implements IConfigurationCell.StationNameImage
            Get
                Return Encoding.ASCII.GetBytes(m_stationName.PadRight(MaximumStationNameLength))
            End Get
        End Property

        Public Overridable ReadOnly Property MaximumStationNameLength() As Integer Implements IConfigurationCell.MaximumStationNameLength
            Get
                ' Typical station name length is 16 characters
                Return 16
            End Get
        End Property

        Public Overridable Property IDCode() As Int16 Implements IConfigurationCell.IDCode
            Get
                Return m_idCode
            End Get
            Set(ByVal Value As Int16)
                m_idCode = Value
            End Set
        End Property

        Public Overridable Property IDLabel() As String Implements IConfigurationCell.IDLabel
            Get
                Return m_idLabel
            End Get
            Set(ByVal Value As String)
                Dim length As Integer = Len(Trim(Value))
                If length > IDLabelLength Or length < IDLabelLength Then
                    Throw New OverflowException("ID label must be exactly " & IDLabelLength & " characters in length")
                Else
                    m_idLabel = Value
                End If
            End Set
        End Property

        Public Overridable ReadOnly Property IDLabelImage() As Byte() Implements IConfigurationCell.IDLabelImage
            Get
                Return Encoding.ASCII.GetBytes(m_idLabel)
            End Get
        End Property

        Public Overridable ReadOnly Property IDLabelLength() As Integer Implements IConfigurationCell.IDLabelLength
            Get
                ' ID label length is 4 characters
                Return 4
            End Get
        End Property

        Public Overridable ReadOnly Property PhasorDefinitions() As PhasorDefinitionCollection Implements IConfigurationCell.PhasorDefinitions
            Get
                Return m_phasorDefinitions
            End Get
        End Property

        Public Overridable Property FrequencyDefinition() As IFrequencyDefinition Implements IConfigurationCell.FrequencyDefinition
            Get
                Return m_frequencyDefinition
            End Get
            Set(ByVal Value As IFrequencyDefinition)
                m_frequencyDefinition = Value
            End Set
        End Property

        Public Overridable ReadOnly Property AnalogDefinitions() As AnalogDefinitionCollection Implements IConfigurationCell.AnalogDefinitions
            Get
                Return m_analogDefinitions
            End Get
        End Property

        Public Overridable ReadOnly Property DigitalDefinitions() As DigitalDefinitionCollection Implements IConfigurationCell.DigitalDefinitions
            Get
                Return m_digitalDefinitions
            End Get
        End Property

        Public Overridable ReadOnly Property SampleRate() As Int16 Implements IConfigurationCell.SampleRate
            Get
                Return Parent.SampleRate
            End Get
        End Property

        Public Overridable Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo

            ' We sort configuration cells by ID code...
            If TypeOf obj Is IConfigurationCell Then
                Return IDCode.CompareTo(DirectCast(obj, IConfigurationCell).IDCode)
            Else
                Throw New ArgumentException("ConfigurationCell can only be compared to other ConfigurationCells")
            End If

        End Function

        Protected Overrides ReadOnly Property BodyLength() As Int16
            Get
                Return m_phasorDefinitions.BinaryLength + m_frequencyDefinition.BinaryLength + m_analogDefinitions.BinaryLength + m_digitalDefinitions.BinaryLength
            End Get
        End Property

        Protected Overrides ReadOnly Property BodyImage() As Byte()
            Get
                Dim buffer As Byte() = Array.CreateInstance(GetType(Byte), BodyLength)
                Dim index As Integer

                ' Copy in common cell image
                CopyImage(m_phasorDefinitions, buffer, index)
                CopyImage(m_frequencyDefinition, buffer, index)
                CopyImage(m_analogDefinitions, buffer, index)
                CopyImage(m_digitalDefinitions, buffer, index)

                Return buffer
            End Get
        End Property

        Protected Overrides Sub ParseBodyImage(ByVal state As IChannelParsingState, ByVal binaryImage As Byte(), ByRef startIndex As Integer)

            Dim parsingState As IConfigurationCellParsingState = state
            Dim x As Integer

            ' TODO: define the common nature of configuration parsing order here...
            '' we are able to "automatically" parse this data out in the data cell base class - BEAUTIFUL!!!
            'With m_configurationCell
            '    For x = 0 To .PhasorDefinitions.Count - 1
            '        m_phasorValues.Add(Activator.CreateInstance(parsingState.PhasorValueType, New Object() {Me, .PhasorDefinitions(x), binaryImage, startIndex}))
            '        startIndex += m_phasorValues(x).BinaryLength
            '    Next

            '    m_frequencyValue = Activator.CreateInstance(parsingState.FrequencyValueType, New Object() {Me, .FrequencyDefinition, binaryImage, startIndex})
            '    startIndex += m_frequencyValue.BinaryLength

            '    For x = 0 To .AnalogDefinitions.Count - 1
            '        m_analogValues.Add(Activator.CreateInstance(parsingState.AnalogValueType, New Object() {Me, .AnalogDefinitions(x), binaryImage, startIndex}))
            '        startIndex += m_analogValues(x).BinaryLength
            '    Next

            '    For x = 0 To .DigitalDefinitions.Count - 1
            '        m_digitalValues.Add(Activator.CreateInstance(parsingState.DigitalValueType, New Object() {Me, .DigitalDefinitions(x), binaryImage, startIndex}))
            '        startIndex += m_digitalValues(x).BinaryLength
            '    Next
            'End With

        End Sub

    End Class

End Namespace