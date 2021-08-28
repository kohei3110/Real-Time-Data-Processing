package com.function;

import com.microsoft.azure.functions.annotation.*;
import com.microsoft.azure.functions.*;
import java.util.*;

/**
 * Azure Functions with Event Hub trigger.
 */
public class EventHubTrigger {
    /**
     * This function will be invoked when an event is received from Event Hub.
     */
    @FunctionName("EventHubTrigger")
    public void run(

        // Event Hub 'eventhub01' has 1 partition.
        @com.microsoft.azure.functions.annotation.EventHubTrigger(name = "message", eventHubName = "eventhub01", connection = "realtimedatastreaming_RootManageSharedAccessKey_EVENTHUB", consumerGroup = "$Default", cardinality = Cardinality.MANY, dataType = "string") List<String> message,

        // Event Hub 'eventhub02' has 32 partitions.
        // @com.microsoft.azure.functions.annotation.EventHubTrigger(name = "message", eventHubName = "eventhub02", connection = "realtimedatastreaming_RootManageSharedAccessKey_EVENTHUB", consumerGroup = "$Default", cardinality = Cardinality.MANY, dataType = "") List<String> message,
        final ExecutionContext context
    ) {
        context.getLogger().info("Length:" + message.size());
        message.forEach(singleMessage -> context.getLogger().info(singleMessage));
    }
}
