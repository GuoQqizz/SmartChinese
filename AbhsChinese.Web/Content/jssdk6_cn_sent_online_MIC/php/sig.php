<?php
header('Content-type: application/json;charset=utf-8');
$alg_arr=array('sha1','sha256','md5');
$alg=@$_POST['alg'];
if($alg==NULL||!in_array($alg, $alg_arr)){
        $alg='sha1';
}
$appKey = "155891795800000f";  //输入驰声授权评测的appkey
$secretKey = "ef59bf9e88da71ec7c341a50a2e723dd";  //输入驰声授权评测的secretKey
$timestamp=floor(microtime(1)*1000);
$rs=json_encode(array('timestamp'=>(string)$timestamp,'sig'=>hash($alg,$appKey . $timestamp . $secretKey)));
die($rs);
?>
