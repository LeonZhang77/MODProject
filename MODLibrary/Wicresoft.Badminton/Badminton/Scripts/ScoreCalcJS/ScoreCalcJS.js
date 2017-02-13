function toCalcToReview() {
    document.ParamaterSetup.action = "/ScoreCalc/CalcToReview";
    document.ParamaterSetup.submit();
}

function toAdjustAccordingToDateRange() {
    document.ParamaterSetup.action = "/ScoreCalc/AdjustAccordingToDateRange";
    document.ParamaterSetup.submit();
}

function toSaveScoreEntry() {
    document.ParamaterSetup.action = "/ScoreCalc/SaveBonusAndScoreEntry";
    document.ParamaterSetup.submit();
}

function toOnlyAdjustAccordingToDateRange() {
    document.ParamaterSetup.action = "/ScoreCalc/OnlyAdjustAccordingToDateRange";
    document.ParamaterSetup.submit();
}

function toSaveUpdateMemberScore() {
    document.ParamaterSetup.action = "/ScoreCalc/SaveScoreAfterAdjustAccordingToDateRange";
    document.ParamaterSetup.submit();
}