--Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf.protobuf"
module('Protol.claine_pb')

CLAINE = protobuf.Descriptor();
CLAINE_ID_FIELD = protobuf.FieldDescriptor();
CLAINE_NAME_FIELD = protobuf.FieldDescriptor();
CLAINE_AGE_FIELD = protobuf.FieldDescriptor();

CLAINE_ID_FIELD.name = "id"
CLAINE_ID_FIELD.full_name = ".Claine.id"
CLAINE_ID_FIELD.number = 1
CLAINE_ID_FIELD.index = 0
CLAINE_ID_FIELD.label = 2
CLAINE_ID_FIELD.has_default_value = false
CLAINE_ID_FIELD.default_value = 0
CLAINE_ID_FIELD.type = 3
CLAINE_ID_FIELD.cpp_type = 2

CLAINE_NAME_FIELD.name = "name"
CLAINE_NAME_FIELD.full_name = ".Claine.name"
CLAINE_NAME_FIELD.number = 2
CLAINE_NAME_FIELD.index = 1
CLAINE_NAME_FIELD.label = 2
CLAINE_NAME_FIELD.has_default_value = false
CLAINE_NAME_FIELD.default_value = ""
CLAINE_NAME_FIELD.type = 9
CLAINE_NAME_FIELD.cpp_type = 9

CLAINE_AGE_FIELD.name = "age"
CLAINE_AGE_FIELD.full_name = ".Claine.age"
CLAINE_AGE_FIELD.number = 3
CLAINE_AGE_FIELD.index = 2
CLAINE_AGE_FIELD.label = 1
CLAINE_AGE_FIELD.has_default_value = true
CLAINE_AGE_FIELD.default_value = 18
CLAINE_AGE_FIELD.type = 5
CLAINE_AGE_FIELD.cpp_type = 1

CLAINE.name = "Claine"
CLAINE.full_name = ".Claine"
CLAINE.nested_types = {}
CLAINE.enum_types = {}
CLAINE.fields = {CLAINE_ID_FIELD, CLAINE_NAME_FIELD, CLAINE_AGE_FIELD}
CLAINE.is_extendable = false
CLAINE.extensions = {}

Claine = protobuf.Message(CLAINE)

