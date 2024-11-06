using Avro.Generic;
using DiceServiceApplication.Domain.Models;

namespace DiceServiceApplication.Kafka.SchemaRegistry;

public class SchemaStatisticsRecordProvider
{
    private static readonly string SCHEMA_STATUS = """
                                                   {
                                                   	"type": "record",
                                                   	"namespace": "ru.reksoft.campus.kafka.statistics",
                                                   	"name": "SessionStatus",
                                                   	"fields": [
                                                   		{
                                                   			"name": "dice",
                                                   			"type": {
                                                   				"type": "enum",
                                                   				"name": "Dice",
                                                   				"symbols": [
                                                   					"D4",
                                                   					"D6",
                                                   					"D8",
                                                   					"D12",
                                                   					"D20"
                                                   				],
                                                   				"default": "D6"
                                                   			}
                                                   		},
                                                   		{
                                                   			"name": "masterId",
                                                   			"type": "string"
                                                   		},
                                                   		{
                                                   			"name": "result",
                                                   			"type": {
                                                   				"type": "map",
                                                   				"values": "boolean"
                                                   			}
                                                   		}
                                                   	]
                                                   }
                                                   """;

    private static readonly Avro.Schema statisticSchema = Avro.Schema.Parse(SCHEMA_STATUS);
    /*
    public static GenericRecord MapSessionData(string sessionId, Dice dice, string masterId, Dictionary<string, bool> resultMap)
    {
	    var statisticRecord = new GenericRecord(statisticSchema);
	    statisticRecord.Add("sessionId", sessionId);
	    statisticRecord.Add(("dice", MapDiceValue(dice));
	    statisticRecord.Add(("masterId", masterId);
	    statisticRecord.Add(("result", resultMap);

	    return statisticRecord;
    }
    
    private static GenericEnumSymbol MapDiceValue(Dice dice)
    {
	    Avro.Schema diceEnumSchema = new SchemaBuilder()
		    .Enumeration("dice")
		    .Namespace("ru.reksoft.campus.kafka.statistics")
		    .Symbols("D4", "D6", "D8", "D12", "D20")
		    .Build();

	    return new GenericEnumSymbol(diceEnumSchema, dice);
    }
    */
}