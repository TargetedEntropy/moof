using System;
using EliteMMO.API;


class Program
{
    static void Main(string[] args)
    {
        int selectedPid = ProcessInfo.GetPolProcessInfo();

        if (selectedPid != -1)
        {
            Console.WriteLine($"Selected Process PID: {selectedPid}");
            EliteAPI api = new EliteAPI(selectedPid);
            
            List<InventoryHelper.ItemDetail> items = InventoryHelper.GetAllContainerItems(api);
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            // JobType PlayerJob = (JobType)Enum.Parse(typeof(JobType), api.Player.GetPlayerInfo().MainJob.ToString());
            // Console.WriteLine(PlayerJob);

        }
        else
        {
            Console.WriteLine("No valid process was selected.");
        }

    }

}

