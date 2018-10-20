Game1.server.sendMessage(farmer.Value.UniqueMultiplayerID, new OutgoingMessage((byte)19, farmer.Value.UniqueMultiplayerID, new object[0]));
Game1.server.playerDisconnected(farmer.Value.UniqueMultiplayerID);
Game1.otherFarmers.Remove(farmer.Value.UniqueMultiplayerID);
//for kicking
class Server_sendMessage_Patcher : Patch
    {
        protected override PatchDescriptor GetPatchDescriptor() => new PatchDescriptor(typeof(GameServer), "sendMessage", new System.Type[] { typeof(long), typeof(OutgoingMessage)});

        public static bool Prefix(long peerId)
        {
            if (Game1.IsServer && (!Game1.otherFarmers.ContainsKey(peerId)))
            {
                //They have been kicked off the server
                return false;
            }

            return true;
        }
    }
    
    
    
    
    // for making sure no messages are received from client
    class Multiplayer_processIncomingMessage_Patcher : Patch
    {
        protected override PatchDescriptor GetPatchDescriptor() => new PatchDescriptor(typeof(Multiplayer), "processIncomingMessage");

        public static bool Prefix(IncomingMessage msg)
        {
            if (Game1.IsServer && (msg == null || !Game1.otherFarmers.ContainsKey(msg.FarmerID)))
            {
                //They have been kicked off the server
                return false;
            }
}
}
