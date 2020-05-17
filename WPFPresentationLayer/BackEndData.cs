using DataLayer;
using DomainLibrary.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace WPFPresentationLayer
{
    public static class BackEndData
    {
        public static List<OverviewData> GetAllTrainings()
        {
            List<OverviewData> datas = new List<OverviewData>();
            datas.AddRange(GetLatestCyclingSessions(5));
            datas.AddRange(GetLatestRunningSessions(5));
            return datas;
        }
        public static List<OverviewData> GetLatestRunningSessions(int count)
        {
            List<OverviewData> datas = new List<OverviewData>();
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            foreach (var x in TM.GetPreviousRunningSessions(count))
                datas.Add(new OverviewData(x));
            return datas;
        }

        internal static Report GenerateMonthReport(int month, int year)
        {
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            return TM.GenerateMonthlyTrainingsReport(year, month);
        }

        public static List<OverviewData> GetLatestCyclingSessions(int count)
        {
            List<OverviewData> datas = new List<OverviewData>();
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            foreach (var x in TM.GetPreviousCyclingSessions(count))
                datas.Add(new OverviewData(x));
            return datas;
        }
        public static List<OverviewData> GetAllCyclingData()
        {
            List<OverviewData> datas = new List<OverviewData>();
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            TM.GetAllCyclingSessions().ForEach(x => datas.Add(new OverviewData(x)));
            return datas;
        }
        public static List<OverviewData> GetAllRunningData()
        {
            List<OverviewData> datas = new List<OverviewData>();
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            TM.GetAllRunningSessions().ForEach(x => datas.Add(new OverviewData(x)));
            return datas;
        }
        private static RunningSession GetBestRunningSession()
        {
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            return TM.GetAllRunningSessions().OrderBy(x => x.Distance).LastOrDefault();
        }
        private static CyclingSession GetBestCyclingSession()
        {
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            return TM.GetAllCyclingSessions().OrderBy(x => x.AverageSpeed).LastOrDefault();
        }
        public static List<OverviewData> GetBestSessions()
        {
            List<OverviewData> datas = new List<OverviewData>();
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            datas.Add(new OverviewData(GetBestCyclingSession()));
            datas.Add(new OverviewData(GetBestRunningSession()));
            return datas;
        }

        internal static void RemoveCyclingSession(int colId)
        {
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            TM.RemoveTrainings(new List<int> { colId }, new List<int> { });
        }

        internal static void RemoveRunningSession(int colId)
        {
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            TM.RemoveTrainings(new List<int> { }, new List<int> { colId });
        }

        internal static void AddRunningTraining(DateTime when, int distance, TimeSpan time, float? avgSpeed, TrainingType tp, string comments)
        {
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            TM.AddRunningTraining(when, distance, time, avgSpeed, tp, comments);
        }

        internal static void AddCyclingTraining(DateTime when, float? distance, TimeSpan time, float? avgSpeed, int? avgWatt, TrainingType tp, BikeType bt, string comments)
        {
            TrainingManager TM = new TrainingManager(new UnitOfWork(new TrainingContext()));
            TM.AddCyclingTraining(when, distance, time, avgSpeed, avgWatt, tp, comments, bt);
        }
    }
}
